using EasyCaching.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Caching;

public static class ServiceCollectionExtensions
{
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