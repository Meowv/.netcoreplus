using Plus.Configuration.Startup;
using System;

namespace Plus.RedisCache
{
    /// <summary>
    /// PlusRedisCacheOptions
    /// </summary>
    public class PlusRedisCacheOptions
    {
        public IPlusStartupConfiguration PlusStartupConfiguration { get; }

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public PlusRedisCacheOptions(IPlusStartupConfiguration plusStartupConfiguration)
        {
            PlusStartupConfiguration = plusStartupConfiguration;

            DatabaseId = GetDefaultDatabaseId();
            ConnectionString = GetDefaultConnectionString();
        }

        private static int GetDefaultDatabaseId()
        {
            try
            {
                var defaultRedisCacheSettings = PlusEngine.Instance.Resolve<DefaultRedisCacheSettings>();
                int databaseId = defaultRedisCacheSettings.DefaultDatabaseId;

                return databaseId;
            }
            catch (Exception ex)
            {
                throw new PlusException(ex.Message);
            }
        }

        private static string GetDefaultConnectionString()
        {
            try
            {
                var defaultRedisCacheSettings = PlusEngine.Instance.Resolve<DefaultRedisCacheSettings>();
                string connectionString = defaultRedisCacheSettings.DefaultConnectionString;

                return connectionString;
            }
            catch (Exception ex)
            {
                throw new PlusException(ex.Message);
            }
        }
    }
}