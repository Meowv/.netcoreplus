using Plus.Configuration;

namespace Plus.RedisCache
{
    /// <summary>
    /// DefaultRedisCacheSettings
    /// </summary>
    public class DefaultRedisCacheSettings : SettingsBase
    {
        public DefaultRedisCacheSettings()
        {
        }

        /// <summary>
        /// DatabaseId
        /// </summary>
        /// <returns></returns>
        public int DefaultDatabaseId => Config.GetSection("RedisCache")["DatabaseId"].ToInt();

        /// <summary>
        /// ConnectionString
        /// </summary>
        /// <returns></returns>
        public string DefaultConnectionString => Config.GetSection("RedisCache")["ConnectionString"].ToString();
    }
}