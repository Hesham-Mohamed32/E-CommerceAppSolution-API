using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistance.Reposteries;
using Services.Abstraction;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ServiceManager (IUnitOfWork _unitOfWork 
        , UserManager<User> _userManager , IOptions<JwtOptions> _options
        , IBasketRepository _basketRepo, IMapper _mapper): IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepo, _mapper));
        private readonly Lazy<IAuthenticationService> _authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager , _options ,_mapper));
        private readonly Lazy<IOrderService> _orderService=new Lazy<IOrderService>(()=> new OrderService(_mapper,_basketRepo,_unitOfWork));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
