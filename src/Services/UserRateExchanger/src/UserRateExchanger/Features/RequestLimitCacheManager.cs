using BuildingBlocks.Caching;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UserRateExchanger.Features;

public class RequestLimitCacheManager : ICacheManager<List<DateTime>>
{
    private readonly ILogger<RequestLimitCacheManager> _logger;
    private readonly IOptions<CacheOptions> _cacheOptions;
    private readonly IEasyCachingProvider _cachingProvider;

    public RequestLimitCacheManager(
        ILogger<RequestLimitCacheManager> logger,
        IOptions<CacheOptions> cacheOptions,
        IEasyCachingProviderFactory factory)
    {
        _logger = logger;
        _cacheOptions = cacheOptions;

        _cachingProvider = factory.GetCachingProvider(_cacheOptions.Value.CacheName);
    }

    public async Task<List<DateTime>> GetAsync(string key, CancellationToken cancellationToken)
    {
        var cacheValue = await _cachingProvider.GetAsync<List<DateTime>>(key, cancellationToken);

        return cacheValue.Value;
    }

    public async Task<bool> IsValidAsync(string key, CancellationToken cancellationToken = default)
    {
        var cachedValues =
            await _cachingProvider.GetAsync<List<DateTime>>(key, cancellationToken);

        if (!cachedValues.HasValue) return true;

        var lowerDateTimeLimit = DateTime.UtcNow.AddMilliseconds(-_cacheOptions.Value.ExpirationTime);

        var numberOfRequestsForTimeLimit =
            cachedValues.Value.Where(x => x >= lowerDateTimeLimit);

        if (numberOfRequestsForTimeLimit.Count() > 10)
        {
            _logger.LogError("User {UserId} exceeded the maximum amount of requests within {TimeLimit}",
                key, _cacheOptions.Value.ExpirationTime);
            return false;
        }

        return true;
    }

    public async Task UpdateAsync(string key, List<DateTime>? data, CancellationToken cancellationToken = default)
    {
        var cachedValues =
            await _cachingProvider.GetAsync<List<DateTime>>(key, cancellationToken);

        // Initialize the list if it's null
        var dateTimestamps = cachedValues.HasValue ? cachedValues.Value : new List<DateTime>();

        dateTimestamps.Add(DateTime.UtcNow);
        await _cachingProvider.SetAsync(key, dateTimestamps,
            TimeSpan.FromMilliseconds(_cacheOptions.Value.ExpirationTime), cancellationToken);
    }
}