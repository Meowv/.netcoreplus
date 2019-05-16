using Plus.Configuration.Startup;
using System;
using System.Collections.Generic;

namespace Plus.Runtime.Caching.Configuration
{
    /// <summary>
    /// 缓存配置接口
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// 获取 Plus 配置对象。
        /// </summary>
        IPlusStartupConfiguration PlusConfiguration { get; }

        IReadOnlyList<ICacheConfigurator> Configurators { get; }

        void ConfigureAll(Action<ICache> initAction);

        void Configure(string cacheName, Action<ICache> initAction);
    }
}