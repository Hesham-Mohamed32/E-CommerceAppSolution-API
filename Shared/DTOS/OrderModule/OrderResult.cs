using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderModule
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public AddressDto shippingAddress { get; init; }
        public ICollection<OrderItemDto> orderItems { get; init; } = new List<OrderItemDto>();
        public string paymentStatus { get; init; } = string.Empty;
        public string deliveryMethod { get; init; }
        public int? DeliveryMethodId { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
    }
}
