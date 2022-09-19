using EasyCaching.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Caching;

/// <summary>
/// The Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the in memory cache.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSection = configuration.GetSection("Cache");

        services.Configure<CacheOptions>(cacheSection);

        var cacheOptions = cacheSection.Get<CacheOptions>();
        return services.AddEasyCaching(options =>
        {
            options.UseInMemory(config =>
            {
                config.DBConfig = new InMemoryCachingOptions()
                {
                    SizeLimit = cacheOptions.SizeLimit
                };
            }, cacheOptions.CacheName);
        });
    }
}