using StackExchange.Redis;
using System;

namespace Plus.RedisCache
{
    /// <summary>
    /// PlusRedisCacheDatabaseProvider
    /// </summary>
    public class PlusRedisCacheDatabaseProvider : IPlusRedisCacheDatabaseProvider
    {
        private readonly PlusRedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// 初始化 <see cref="PlusRedisCacheDatabaseProvider"/> 一个实例
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionMultiplexer"></param>
        public PlusRedisCacheDatabaseProvider(PlusRedisCacheOptions options, Lazy<ConnectionMultiplexer> connectionMultiplexer)
        {
            _options = options;
            _connectionMultiplexer = connectionMultiplexer;
        }

        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }
    }
}