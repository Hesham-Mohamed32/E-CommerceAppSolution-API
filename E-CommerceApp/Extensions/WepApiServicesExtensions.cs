using E_CommerceApp.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Extensions
{
    public static class WepApiServicesExtensions
    {
        public static IServiceCollection AddWepApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            return services;
        }
    }
}
