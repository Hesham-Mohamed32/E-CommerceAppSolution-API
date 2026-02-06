using DomainLayer.Contracts;
using DomainLayer.Entities.IdentityModule;
using DomainLayer.Entities.OrderModule;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext
        , RoleManager<IdentityRole> _roleManager
        , UserManager<User> _userManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
            { 
                _dbContext.Database.Migrate();
            }
            try
            {
                #region Seeding Data
                #region Product Brands
                if (!_dbContext.ProductBrands.Any())
                {
                    var BrandsData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/brands.json");
                    var Brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                    if (Brands is not null && Brands.Any())
                    {
                    await _dbContext.ProductBrands.AddRangeAsync(Brands);
                    }
                }
                #endregion
                #region Product Type
                if (!_dbContext.ProductTypes.Any())
                {
                    var TypesData = File.OpenRead("../Infrastructure/Persistance/Data/DataSeed/types.json");
                    var Types =await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                    if (Types is not null && Types.Any())
                    {
                      await  _dbContext.ProductTypes.AddRangeAsync(Types);
                    }

                }
                #endregion
                #region Product
                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead("../InfraStructure/Persistance/Data/DataSeed/products.json");
                    var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                       await _dbContext.Products.AddRangeAsync(Products);
                    }
                }
                #endregion
                #region Delivery Methods 
                if (!_dbContext.DeliveryMethods.Any())
                {
                    var deliveryMethod = File.OpenRead("../InfraStructure/Persistance/Data/DataSeed/delivery.json");
                    var delvieryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryMethod);
                    if (delvieryMethods is not null && delvieryMethods.Any())
                    {
                        await _dbContext.DeliveryMethods.AddRangeAsync(delvieryMethods);
                    }
                }
                #endregion

                await _dbContext.SaveChangesAsync();
                #endregion
            }
            catch (Exception ex)
            {

                //Handale Exexption
            }
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                //1- Roles [Admin , Super Admin]
                if(! _roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                //2- Seed Users [AdminUser , SuperAdminUser]
                if(! _userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email ="Admin@gmail.com",
                        PhoneNumber = "1234567890",

                    };
                    var SuperAdminUser = new User
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "1234567890",

                    };
                    await _userManager.CreateAsync(adminUser, "P@@1122a");
                    await _userManager.CreateAsync(SuperAdminUser, "P@@3344a");
                    //3- Assign Roles ==> Users
                    await _userManager.AddToRoleAsync( adminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }
            }
               
            catch (Exception)
            {
                throw; 
            }
        }
    }
}
