namespace BuildingBlocks.Caching;

public interface ICacheManager<in T>
{
    bool IsValid(T value);
    bool Update(T value);
}