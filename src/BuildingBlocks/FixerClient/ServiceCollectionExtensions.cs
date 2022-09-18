using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.FixerClient
{
    /// <summary>
    /// Configures the FixerClient.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddFixerClient(this IServiceCollection services, IConfiguration configuration)
        {
            var fixerSection = configuration.GetSection("Fixer");

            services.Configure<FixerOptions>(fixerSection);
            services.AddTransient<IFixerClient, FixerClient>();
        }
    }
}
