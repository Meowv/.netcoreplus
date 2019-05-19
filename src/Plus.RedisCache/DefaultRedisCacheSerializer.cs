using Plus.Dependency;
using StackExchange.Redis;
using System;

namespace Plus.RedisCache
{
    /// <summary>
    /// DefaultRedisCacheSerializer
    /// </summary>
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer, ITransientDependency
    {
        /// <summary>
        /// Deserialize
        /// </summary>
        /// <param name="objbyte"></param>
        /// <returns></returns>
        public virtual object Deserialize(RedisValue objbyte)
        {
            var serializedObj = objbyte.ToString();

            return serializedObj.DeserializeWithType();
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual string Serialize(object value, Type type)
        {
            return value.SerializeWithType(type);
        }
    }
}