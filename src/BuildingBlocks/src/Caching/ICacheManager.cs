namespace BuildingBlocks.Caching;

/// <summary>
/// The Cache Manager.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICacheManager<T>
{
    /// <summary>
    /// Get the cache item.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task<T?> GetAsync(string key, CancellationToken cancellationToken);

    /// <summary>
    /// Validates the value in the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task<bool> IsValidAsync(string key, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the values in the cache.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="data">The data to be stored.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task UpdateAsync(string key, T? data, CancellationToken cancellationToken);
}