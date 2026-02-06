using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Entities.BasketModule;
using DomainLayer.Entities.OrderModule;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using Persistance.Reposteries;
using Services.Abstraction;
using Services.Specifications;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = DomainLayer.Entities.OrderModule.Address;

namespace Services.Implementation
{
    internal class OrderService (IMapper _mapper , IBasketRepository _basketRepository ,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAysnc(OrderRequest request, string userEmail)
        {

            //Shipping Address
            var shippingAddress = _mapper.Map<ShippingAddress>(request.ShippingAddress);
            //OrderItem
            var basket = await _basketRepository.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);
            var orderItem = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItem.Add(CreateOrderItems(product,item));
            
            }
            //Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
            //SubTotal 
            var subTotal = orderItem.Sum(item=>item.Price * item.Quantity);
            //Create Order 
            var order = new Order(userEmail,shippingAddress,orderItem,deliveryMethod,subTotal);
            //Save Changes
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            // Map , Return
            return _mapper.Map<OrderResult>(order);
            



        }

        private OrderItem CreateOrderItems(Product product, BasketItem item)
        {
            var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(productInOrderItem, product.Price, item.Quantity);
        }
        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }
        public async Task<OrderResult> GetOrderByIdAsync(Guid Id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                 .GetByIdAsync(new OrderWithIncludesSpecification(Id)) ?? throw new OrderNotFoundExceptions(Id);
            return _mapper.Map<OrderResult>(order);
        }
        public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsyns(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderWithIncludesSpecification(userEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }


    }
}
