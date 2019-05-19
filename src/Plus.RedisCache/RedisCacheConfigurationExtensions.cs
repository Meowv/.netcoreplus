using Plus.Dependency;
using Plus.Runtime.Caching;
using Plus.Runtime.Caching.Configuration;
using System;

namespace Plus.RedisCache
{
    /// <summary>
    /// RedisCacheConfigurationExtensions
    /// </summary>
    public static class RedisCacheConfigurationExtensions
    {
        public static void UseRedis(this ICachingConfiguration cachingConfiguration)
        {
            cachingConfiguration.UseRedis(options => { });
        }

        public static void UseRedis(this ICachingConfiguration cachingConfiguration, Action<PlusRedisCacheOptions> optionsAction)
        {
            var iocManager = cachingConfiguration.PlusConfiguration.IocManager;

            iocManager.RegisterIfNot<ICacheManager, PlusRedisCacheManager>();

            optionsAction(iocManager.Resolve<PlusRedisCacheOptions>());
        }
    }
}