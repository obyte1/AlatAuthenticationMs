using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AlatAuth.Business.Caching
{
    public class CachingService : ICachingService
    {
        readonly ObjectCache _memoryCache;
        public CachingService()
        {
            _memoryCache = MemoryCache.Default;
        }
        public T GetData<T>(string key)
        {
            T item = (T)_memoryCache.Get(key);
            return item;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Set(key, value, expirationTime);
            }
            return true;
        }
        public object RemoveData(string key)
        {

            if (!string.IsNullOrEmpty(key))
            {
                return _memoryCache.Remove(key);
            }
            return null;
        }
    }
}
