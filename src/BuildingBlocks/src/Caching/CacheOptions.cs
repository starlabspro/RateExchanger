namespace BuildingBlocks.Caching;

public class CacheOptions
{
    /// <summary>
    /// Cache Name.
    /// </summary>
    public string CacheName { get; set; }

    /// <summary>
    /// Cache size limit.
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// Cache Expiration Time in Milliseconds. Default is 30 minutes.
    /// </summary>
    public int ExpirationTime { get; set; } = 1_800_000;
}