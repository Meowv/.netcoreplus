using Microsoft.Extensions.Configuration;
using System.IO;

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

        public static string MySqlConnectionString => _config["ConnectionStrings:MySql"];
    }
}