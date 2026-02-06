using AutoMapper;
using DomainLayer.Models;
using Shared.DTOS.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles() 
        {
            #region Product
            CreateMap<Product, ProductDto>()
          .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
          .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand, BrandDto>();
            #endregion

        }
    }
}
