using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Entities.BasketModule;
using DomainLayer.Exceptions;
using Persistance.Reposteries;
using Services.Abstraction;
using Shared.DTOS.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
           var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return CreateOrUpdateBasket is null ? throw new Exception ("Can Not Create Or Update the Basket") :
                _mapper.Map<BasketDto>(CreateOrUpdateBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
           var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) :
                _mapper.Map<BasketDto>(basket);
        }
    }
}
