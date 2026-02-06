using Shared.DTOS.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        //GetOrderById ==> Take Guid Id ==> Return OrderResult
        Task<OrderResult> GetOrderByIdAsync(Guid Id);
        //GetOrderByEmail ==> Take String UserEmail ==> Return IEnmerable<OrderResult>
        Task<IEnumerable<OrderResult>> GetOrderByEmailAsyns(string UserEmail);
        //CreatOrder ==> Take OrderRquest ==> Return OrderResult
        Task<OrderResult> CreateOrderAysnc(OrderRequest order, string UserEmail);
        //GetDileveryMethod ==> () ==> Return IEnmerable<DileveryMethodResult>
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}
