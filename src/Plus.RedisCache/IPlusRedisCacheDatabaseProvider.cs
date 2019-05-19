using StackExchange.Redis;

namespace Plus.RedisCache
{
    /// <summary>
    /// 用于获取 <see cref="IDatabase"/> Redis cache。
    /// </summary>
    public interface IPlusRedisCacheDatabaseProvider
    {
        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();
    }
}