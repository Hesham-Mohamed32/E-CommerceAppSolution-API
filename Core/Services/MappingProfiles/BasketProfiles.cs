using AutoMapper;
using DomainLayer.Entities.BasketModule;
using Shared.DTOS.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
     public class BasketProfiles : Profile
    {
        public BasketProfiles() 
        { 
        CreateMap<CustomerBasket,BasketDto>().ReverseMap();
        CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}
