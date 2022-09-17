using BuildingBlocks.Caching.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Caching.Service
{
    public class CacheManager : ICacheManager
    {
        private Dictionary<string, object> _items = null;
        public CacheManager()
        {
            _items = new Dictionary<string, object>();
        }

        private void TimerProc(object state)
        {
            //Remove the cached object
            string key = state.ToString();
            Remove(key);
        }

        public void Add(string key, object obj, int cacheTime = Timeout.Infinite)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key is null.");

            if (_items.Keys.Contains(key))
                throw new ArgumentException("An element with the same key already exists.");

            //Set timer
            Timer t = new Timer(new TimerCallback(TimerProc),key, cacheTime, Timeout.Infinite);

            _items.Add(key, obj);
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key is null.");

            return _items.Remove(key);
        }

        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key is null.");

            if (!_items.Keys.Contains(key))
                throw new KeyNotFoundException("key does not exist in the collection.");

            return _items[key];
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
