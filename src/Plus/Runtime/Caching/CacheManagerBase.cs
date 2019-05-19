using Plus.Dependency;
using Plus.Runtime.Caching.Configuration;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Plus.Runtime.Caching
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager, ISingletonDependency
    {
        protected readonly IIocManager IocManager;

        protected readonly ICachingConfiguration Configuration;

        protected readonly ConcurrentDictionary<string, ICache> Caches;

        protected CacheManagerBase(IIocManager iocManager, ICachingConfiguration configuration)
        {
            IocManager = iocManager;
            Configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }

        public virtual ICache GetCache(string name)
        {
            return Caches.GetOrAdd(name, (cacheName) =>
            {
                var cache = CreateCacheImplementation(cacheName);

                var configurators = Configuration.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);

                foreach (var configurator in configurators)
                {
                    configurator.InitAction?.Invoke(cache);
                }

                return cache;
            });
        }

        public virtual void Dispose()
        {
            DisposeCaches();
            Caches.Clear();
        }

        protected virtual void DisposeCaches()
        {
            foreach (var cache in Caches)
            {
                IocManager.Release(cache.Value);
            }
        }

        /// <summary>
        /// 用于创建实际的缓存实现
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns></returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}