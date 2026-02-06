using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ValidationErrorResponse
    {
        public int StatesCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public IEnumerable<ValidationError> Errors { get; set; } = [];
    }
}
