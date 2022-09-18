using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Caching.Contract
{
    public interface ICacheManager
    {
        bool Remove(string key);
        object Get(string key);
        public void Insert(int userId);
        void Clear();
    }
}
