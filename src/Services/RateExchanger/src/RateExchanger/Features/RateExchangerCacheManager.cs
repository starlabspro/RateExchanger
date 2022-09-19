using BuildingBlocks.Caching;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RateExchanger.Features;

public class RateExchangerCacheManager : ICacheManager<Dictionary<string, decimal>?>
{
    private readonly ILogger<RateExchangerCacheManager> _logger;
    private readonly IOptions<CacheOptions> _cacheOptions;
    private readonly IEasyCachingProvider _cachingProvider;

    public RateExchangerCacheManager(
        ILogger<RateExchangerCacheManager> logger,
        IOptions<CacheOptions> cacheOptions,
        IEasyCachingProviderFactory factory)
    {
        _logger = logger;
        _cacheOptions = cacheOptions;

        _cachingProvider = factory.GetCachingProvider(_cacheOptions.Value.CacheName);
    }

    public async Task<Dictionary<string, decimal>?> GetAsync(string key, CancellationToken cancellationToken)
    {
        var cachedExchangeRates =
            await _cachingProvider.GetAsync<Dictionary<string, decimal>>(key, cancellationToken);

        return cachedExchangeRates?.Value;
    }

    public Task<bool> IsValidAsync(string key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(string key, Dictionary<string, decimal>? data, CancellationToken cancellationToken)
    {
        await _cachingProvider.SetAsync(key, data,
            TimeSpan.FromMilliseconds(_cacheOptions.Value.ExpirationTime), cancellationToken);
    }
}