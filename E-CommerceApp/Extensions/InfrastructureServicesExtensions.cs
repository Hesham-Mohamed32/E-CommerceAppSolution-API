using DomainLayer.Contracts;
using DomainLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance.Data;
using Persistance.Data.DataSeed;
using Persistance.Identity;
using Persistance.Reposteries;
using Shared.Common;
using StackExchange.Redis;
using System.Text;

namespace E_CommerceApp.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DeafultConnetion"));
            });
            services.AddDbContext<IdentityStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnetion"));
            });
            services.AddScoped<IDataSeeding, DataSeeding>();
           services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
                {
                    return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedicConnection")!);
            });
            services.AddIdentity<User, IdentityRole>(options=>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityStoreDbContext>();
                //.AddDefaultTokenProviders();
           services.AddScoped<IBasketRepository, BasketRepository>();
            services.ValidateJwt(configuration);
            return services;
        }
        public static IServiceCollection ValidateJwt(this IServiceCollection services , IConfiguration configuration )
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issure,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                };
            
            });
            services.AddAuthorization();
            return services;
            
        }
    }
}
