using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException :NotFoundException
    {
        public DeliveryMethodNotFoundException(int id) : base($"The delviery Method With id {id} Not Found") 
        { 
        }

    }
}
