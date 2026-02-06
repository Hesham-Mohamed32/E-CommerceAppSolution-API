using DomainLayer.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Reposteries
{
    public interface IBasketRepository
    {
        //Get Basket By Id
        Task<CustomerBasket> GetBasketAsync(string id);
        //Creat Or Update
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? timeToLive=null);
        //Delete
        Task<bool> DeleteBasketAsync(string id);
    }
}
