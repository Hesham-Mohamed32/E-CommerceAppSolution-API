using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = DomainLayer.Entities.OrderModule.Address;

namespace DomainLayer.Entities.OrderModule
{
    public class Order :BaseEntity<Guid>
    {
        public Order(string userEmail, ShippingAddress shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            Id= Guid.NewGuid();
            UserEmail = userEmail;
            this.shippingAddress = shippingAddress;
            this.orderItems = orderItems;
            this.deliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }
        public Order() 
        {
            
        } 

        public string UserEmail { get; set; } = string.Empty; 
        public ShippingAddress shippingAddress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus paymentStatus { get; set; }= OrderPaymentStatus.Pending;
        public DeliveryMethod deliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; set; }=string.Empty;  
    }
}
