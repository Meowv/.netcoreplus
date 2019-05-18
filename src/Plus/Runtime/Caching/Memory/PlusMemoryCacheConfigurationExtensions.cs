using Plus.Dependency;
using Plus.Runtime.Caching.Configuration;

namespace Plus.Runtime.Caching.Memory
{
    public static class PlusMemoryCacheConfigurationExtensions
    {
        public static void UseMemoryCache(this ICachingConfiguration cachingConfiguration)
        {
            IIocManager iocManager = cachingConfiguration.PlusConfiguration.IocManager;
            iocManager.RegisterIfNot<ICacheManager, PlusMemoryCacheManager>();
        }
    }
}