using Shared.DTOS.IdentityModule;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        //Log in
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Register
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        //GetCurrentUser
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        //CheckEmailExist
        Task<bool> CheckEmailExistAsync(string userEmail);
        //GetUserAddress
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        //UpdateUserAddress
        Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
    }
}
