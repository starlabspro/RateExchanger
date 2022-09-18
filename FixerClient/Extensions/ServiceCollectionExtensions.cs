using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using FixerClient.Refit;
using FixerClient.Options;

namespace FixerClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFixerClient(this IServiceCollection services, IConfiguration configuration)
        {
            var fixerSection = configuration.GetSection("Fixer");

            var fixerOptions = fixerSection.Get<FixerOptions>();

            services.Configure<FixerOptions>(fixerSection);

            services.AddSingleton<IFixerProvider, FixerProvider>();

            services.AddRefitClient<IFixerRefitClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(fixerOptions.BaseUri));
        }
    }
}
