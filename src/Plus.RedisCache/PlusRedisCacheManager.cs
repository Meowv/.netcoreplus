using Plus.Dependency;
using Plus.Runtime.Caching;
using Plus.Runtime.Caching.Configuration;

namespace Plus.RedisCache
{
    /// <summary>
    /// PlusRedisCacheManager
    /// </summary>
    public class PlusRedisCacheManager : CacheManagerBase
    {
        public PlusRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<PlusRedisCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<PlusRedisCache>();
        }
    }
}