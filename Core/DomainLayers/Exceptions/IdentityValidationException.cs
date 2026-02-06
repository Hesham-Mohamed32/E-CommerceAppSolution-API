using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DomainLayer.Exceptions
{
    public sealed class IdentityValidationException : Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];
        public IdentityValidationException(IEnumerable<string>errors) : base("Validation Failed")
        {
            Errors = errors ;
        }
        
    }
}
