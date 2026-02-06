using Shared.DTOS.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        //Get
        Task<BasketDto> GetBasketAsync(string id);
        //Delete
        Task<bool> DeleteBasketAsync(string id);
        //CreateOrUpdate
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
    }
}
