using DomainLayer.Contracts;
using E_CommerceApp.MiddelWares;

namespace E_CommerceApp.Extensions
{
    public static class WepApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var ObjetOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjetOfDataSeeding.DataSeedAsync();
            await ObjetOfDataSeeding.SeedIdentityDataAsync();
            return app;
        }
        public static WebApplication UseExeptionHandalingMiddelWares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddelWare>();
            return app;
        }
        public static WebApplication UseSwaggerMiddelWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
