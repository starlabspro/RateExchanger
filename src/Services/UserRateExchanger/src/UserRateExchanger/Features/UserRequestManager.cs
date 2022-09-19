using BuildingBlocks.Caching;
using EasyCaching.Core;

namespace UserRateExchanger.Features;

public class UserRequestManager<CacheValue> : ICacheManager<CacheValue>
{
    public bool IsValid(CacheValue value)
    {
        throw new NotImplementedException();
    }

    public bool Update(CacheValue value)
    {
        throw new NotImplementedException();
    }
}