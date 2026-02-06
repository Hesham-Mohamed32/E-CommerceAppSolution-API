using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class OrderNotFoundExceptions : NotFoundException
    {
        public OrderNotFoundExceptions(Guid Id) : base($"Order With Id {Id} Not Found")
        { 
        }
    }
}
