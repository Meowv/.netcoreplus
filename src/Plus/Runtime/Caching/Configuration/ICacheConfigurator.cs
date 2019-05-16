using System;

namespace Plus.Runtime.Caching.Configuration
{
    /// <summary>
    /// 已注册的缓存配置
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// 缓存的名称
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// 配置操作，在创建缓存之后调用
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}