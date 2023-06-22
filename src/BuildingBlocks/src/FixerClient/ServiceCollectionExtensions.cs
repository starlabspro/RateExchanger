using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.FixerClient;

/// <summary>
/// The Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Fixer client.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddFixerClient(this IServiceCollection services, IConfiguration configuration)
    {
        var fixerSection = configuration.GetSection("Fixer");

        services.Configure<FixerOptions>(fixerSection);
        services.AddTransient<IFixerClient, FixerClient>();

        return services;
    }
}