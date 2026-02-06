using AutoMapper;
using DomainLayer.Entities.IdentityModule;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared.Common;
using Shared.DTOS.IdentityModule;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class AuthenticationService (UserManager<User> _userManager 
        , IOptions<JwtOptions> _options , IMapper _mapper): IAuthenticationService
    {
        public async Task<bool> CheckEmailExistAsync(string userEmail)
        {
           var user = await _userManager.FindByEmailAsync(userEmail);
            return user != null;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
        {
           var user= await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);
            return new UserResultDto(user.DisplayName , await CreateTokenAsync(user) , user.Email);
        }

        public async Task<AddressDto> GetUserAddressAsync(string userEmail)
        {
           var user = await _userManager.Users.Include(u=>u.Address)
                .FirstOrDefaultAsync(u=>u.Email==userEmail) ?? throw new UserNotFoundException(userEmail);
            return _mapper.Map<AddressDto>(user.Address);
        }
        public async Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Email == userEmail) ?? throw new UserNotFoundException(userEmail);
            if(user.Address != null) //Update
            { 
                user.Address.FirstName = addressDto .FirstName;
                user.Address.LastName = addressDto .LastName;
                user.Address.Country = addressDto .Country;
                user.Address.City = addressDto .City;
                user.Address.Street = addressDto .Street;
            }
            else //Create
            {
                var address =  _mapper.Map<Address>(addressDto);
                user.Address = address;
            } 
            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Email Already Exist == [Exist ==> acc]
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnauthorizedException();
            //Check Password
           var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!result) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName,await CreateTokenAsync(user),user.Email);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName=registerDto.DispalyName,
                 Email=registerDto.Email,
                 UserName=registerDto.UserName,
                 PhoneNumber=registerDto.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);
            //Validate
            if(!result.Succeeded)
            {
                var errrors= result.Errors.Select(e=>e.Description).ToList();
                throw new  IdentityValidationException(errrors);
            }
            return new UserResultDto(registerDto.DispalyName, await CreateTokenAsync(user), registerDto.Email);
        }

        //Token ==>   Encrypted String ==> function return string
        //Helper method
        //Claims
        //Name , Email , roles [m-m] 
        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = _options.Value;
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)

            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            //[Algorithm + Key]
            var SignInCreds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: jwtOptions.Issure, audience: jwtOptions.Audience, claims: claims, expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays), signingCredentials: SignInCreds);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
