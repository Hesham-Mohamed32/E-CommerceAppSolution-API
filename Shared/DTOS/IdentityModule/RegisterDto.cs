using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.IdentityModule
{
    public record RegisterDto
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; init; } 
        public string DispalyName { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
    }
}
