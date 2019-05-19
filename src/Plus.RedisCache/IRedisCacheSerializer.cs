using StackExchange.Redis;
using System;

namespace Plus.RedisCache
{
    /// <summary>
    /// (De)Serialize
    /// </summary>
    public interface IRedisCacheSerializer
    {
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="objbyte"></param>
        /// <returns></returns>
        object Deserialize(RedisValue objbyte);

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string Serialize(object value, Type type);
    }
}