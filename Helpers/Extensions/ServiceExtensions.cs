using KitchenApi.Interfaces;
using KitchenApi.Models;
using Microsoft.Extensions.Options;

namespace KitchenApi.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning();
        }
        public static void ConfigureDatabaseSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<KitchenDatabaseSettings>(
        Configuration.GetSection(nameof(KitchenDatabaseSettings)));

            services.AddSingleton<IKitchenDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<KitchenDatabaseSettings>>().Value);
        }
    }
}
