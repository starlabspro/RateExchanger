namespace BuildingBlocks.Caching;

public class CacheOptions
{
    public string CacheName { get; set; }
    public int SizeLimit { get; set; }
    public int ExpirationTime { get; set; } = 1_800_000;
}