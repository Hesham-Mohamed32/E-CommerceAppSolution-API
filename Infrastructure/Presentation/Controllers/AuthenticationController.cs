using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOS.IdentityModule;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController (IServiceManager _serviceManager): ApiController
    {
        //Post ==>Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
            => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));
        //Post ==>Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        =>  Ok(await _serviceManager.AuthenticationService.CheckEmailExistAsync(email));
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetUserAddressAsync(email);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>>UpdateUserAddressAsync(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.UpdateUserAddressAsync(email, addressDto);
            return Ok(address);
        }
          
        
    }
}
