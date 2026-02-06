using DomainLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Identity
{
    public class IdentityStoreDbContext : IdentityDbContext
    {
        public IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}
