namespace BuildingBlocks.Caching;

public interface ICacheManager<T>
{
    Task<T?> GetAsync(string key, CancellationToken cancellationToken);
    Task<bool> IsValidAsync(string key, CancellationToken cancellationToken);
    Task UpdateAsync(string key, T? data, CancellationToken cancellationToken);
}