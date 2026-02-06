using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class BasketNotFoundException : Exception
    {
        public BasketNotFoundException(string id) : base($"Baket With id : {id} Not Found") 
        {
        }
    }
}
