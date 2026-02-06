using AutoMapper;
using DomainLayer.Entities.OrderModule;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = DomainLayer.Entities.OrderModule.Address;
using IdentityAddress = DomainLayer.Entities.IdentityModule.Address;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<IdentityAddress, AddressDto>().ReverseMap();
            CreateMap<DeliveryMethod,DeliveryMethodResult>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl));
            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.paymentStatus, options => options.MapFrom(src => src.paymentStatus.ToString()))
             .ForMember(dest => dest.deliveryMethod, options => options.MapFrom(src => src.deliveryMethod.ShortName))
             .ForMember(dest=>dest.Total , options=>options.MapFrom(src=>src.SubTotal + src.deliveryMethod.Price));




        }
    }
}
