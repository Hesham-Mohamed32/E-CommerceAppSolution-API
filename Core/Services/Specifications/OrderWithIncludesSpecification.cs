using DomainLayer.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecification : BaseSpecifications<Order,Guid>
    {
        public OrderWithIncludesSpecification(Guid Id) : base(o=>o.Id == Id) 
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.deliveryMethod);
        }
        public OrderWithIncludesSpecification(string userEmail) : base(o=>o.UserEmail==userEmail)
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.deliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
