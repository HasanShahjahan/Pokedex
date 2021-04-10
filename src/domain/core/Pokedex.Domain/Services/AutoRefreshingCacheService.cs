using Microsoft.Extensions.Caching.Memory;
using Pokedex.DataObjects.Settings;
using Pokedex.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;

namespace Pokedex.Domain.Services
{
    public class AutoRefreshingCacheService : IAutoRefreshingCacheService
    {
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public AutoRefreshingCacheService(AppSettings appSettings, IMemoryCache memoryCache)
        {
            _appSettings = appSettings;
            _memoryCache = memoryCache;
        }

        public Hashtable CheckCache(string pokemonName)
        {
            _memoryCache.TryGetValue(pokemonName, out Hashtable shortenUrlHashtable);
            return shortenUrlHashtable;
        }

        public void SetCache(string pokemonName, string value)
        {
            // Normal lock doesn't work in async code
            if (!_memoryCache.TryGetValue(pokemonName, out Hashtable shortenUrlHashtable))
            {
                SemaphoreSlim certLock = locks.GetOrAdd(pokemonName, k => new SemaphoreSlim(1, 1));
                certLock.WaitAsync();
                try
                {
                    if (!_memoryCache.TryGetValue(pokemonName, out shortenUrlHashtable))
                    {
                        Hashtable hashtable = new Hashtable
                        {
                            { "OriginalUrl", pokemonName },
                            { "Value", value }
                        };
                        _memoryCache.Set(pokemonName, hashtable, GetMemoryCacheEntryOptions(hashtable, _appSettings.MemoryCache.RefreshTimeInDays));
                    }
                }
                finally
                {
                    certLock.Release();
                }
            }
        }

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(Hashtable hashtable, int expireInDays = 1)
        {
            var expirationTime = DateTime.Now.AddDays(expireInDays);
            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(expirationTime);

            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = (key, value, reason, state) =>
                {
                    if (reason == EvictionReason.TokenExpired || reason == EvictionReason.Expired)
                    {
                        var newValue = hashtable;
                        if (newValue != null)
                        {
                            _memoryCache.Set(key, newValue, GetMemoryCacheEntryOptions(hashtable, expireInDays));
                        }
                        else
                        {
                            _memoryCache.Set(key, value, GetMemoryCacheEntryOptions(hashtable, expireInDays));
                        }
                    }
                }
            });
            return options;
        }
    }
}
