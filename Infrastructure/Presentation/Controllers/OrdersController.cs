using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        //Create Order

        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest request)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.CreateOrderAysnc(request, userEmail);
            return Ok(order);
        }
        //GetOrderByID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
        { 
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        //GetOrderByEmail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmail()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders =await  _serviceManager.OrderService.GetOrderByEmailAsyns(userEmail);
            return Ok(orders);
        }
        //GetDeliveryMethods
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
