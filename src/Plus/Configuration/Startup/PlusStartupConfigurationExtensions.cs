using Plus.Dependency;
using System;

namespace Plus.Configuration.Startup
{
    /// <summary>
    /// PlusStartupConfigurationExtensions
    /// </summary>
    public static class PlusStartupConfigurationExtensions
    {
        public static void ReplaceService(this IPlusStartupConfiguration configuration, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            configuration.ReplaceService(type, delegate
            {
                configuration.IocManager.Register(type, impl, lifeStyle);
            });
        }

        public static void ReplaceService<TType, TImpl>(this IPlusStartupConfiguration configuration, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class where TImpl : class, TType
        {
            configuration.ReplaceService(typeof(TType), delegate
            {
                configuration.IocManager.Register<TType, TImpl>(lifeStyle);
            });
        }

        public static void ReplaceService<TType>(this IPlusStartupConfiguration configuration, Action replaceAction) where TType : class
        {
            configuration.ReplaceService(typeof(TType), replaceAction);
        }
    }
}