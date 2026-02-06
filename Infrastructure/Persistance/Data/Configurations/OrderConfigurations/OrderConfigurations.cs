using DomainLayer.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations.OrderConfigurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.shippingAddress, sh => sh.WithOwner());
            builder.HasMany(o => o.orderItems).WithOne();
            builder.Property(o=>o.paymentStatus).
                HasConversion(ps=>ps.ToString(),ps=>Enum.Parse<OrderPaymentStatus>(ps));
            builder.HasOne(o => o.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,4)");
        }
    }
}
