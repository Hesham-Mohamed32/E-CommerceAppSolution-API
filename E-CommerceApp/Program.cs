
using DomainLayer.Contracts;
using E_CommerceApp.Extensions;
using E_CommerceApp.Factories;
using E_CommerceApp.MiddelWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Data.DataSeed;
using Persistance.Reposteries;
using Services;
using Services.Abstraction;

namespace E_CommerceApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region DI Container
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            #region Configure Services
            //InfrastructureServices 
            builder.Services.AddInfrastructureServices(builder.Configuration);
            
            //Core Servisces
            builder.Services.AddCoreServices(builder.Configuration);

            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //Wep Api Services
            builder.Services.AddWepApiServices();
            #endregion
            #region Pipe Lines - MiddelWares 
            //App
            var app = builder.Build();
            await app.SeedDatabaseAsync();

            // Configure the HTTP request pipeline.
            app.UseExeptionHandalingMiddelWares();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddelWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
