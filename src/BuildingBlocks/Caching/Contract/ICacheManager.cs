using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Caching.Contract
{
    public interface ICacheManager
    {
        void Add(string key, object obj, int cacheTime);
        bool Remove(string key);
        object Get(string key);
        void Clear();
    }
}
