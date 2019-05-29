using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Plus.Core.Tests.Configuration
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot _config;

        static AppSettings()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", true, true);

            _config = builder.Build();
        }

        private static IConfigurationSection ConnectionStrings => _config.GetSection("ConnectionStrings");

        public static string MySqlConnectionString => ConnectionStrings["MySql"];

        public static class MongoDb
        {
            private static IConfigurationSection MongoDbSection => _config.GetSection("MongoDb");

            public static string ConnectionMode => MongoDbSection["ConnectionMode"];

            public static string DatabaseName => MongoDbSection["DatabaseName"];

            public static string Username => MongoDbSection["Username"];

            public static string Password => MongoDbSection["Password"];

            public static IList<MongoDbServerAddress> Servers
            {
                get
                {
                    var arrays = MongoDbSection.GetSection("Servers").GetChildren().Select(x => x.Value).ToArray();
                    IList<MongoDbServerAddress> results = new List<MongoDbServerAddress>();
                    foreach (var str in arrays)
                    {
                        var item = str.Replace("：", ":").Split(':');
                        if (item.Length == 2)
                        {
                            results.Add(new MongoDbServerAddress() { Host = item[0], Port = item[1].ToInt() });
                        }
                    }

                    return results;
                }
            }
        }
    }
}