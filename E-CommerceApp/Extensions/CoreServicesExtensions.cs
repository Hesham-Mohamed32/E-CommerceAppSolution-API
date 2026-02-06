using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Abstraction;
using Services.Implementation;
using Services.MappingProfiles;
using Shared.Common;

namespace E_CommerceApp.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration )
        {
            //services.AddAutoMapper(X => X.AddProfile(new ProductMappingProfiles()));
            //services.AddAutoMapper(X => X.AddProfile(new BasketProfiles()));
            // طريقة شات جي بي تي 
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IServiceManager, ServiceManager>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
