using Microsoft.Extensions.Configuration;
using Plus.Dependency;
using System.IO;

namespace Plus.Configuration
{
    /// <summary>
    /// SettingsBase
    /// </summary>
    public class SettingsBase : ISingletonDependency
    {
        public IConfigurationRoot _config;

        public IConfigurationRoot Config => _config;

        public SettingsBase()
        {
            IConfigurationBuilder configurationBuilder = JsonConfigurationExtensions.AddJsonFile(FileConfigurationExtensions.SetBasePath(new ConfigurationBuilder(), Directory.GetCurrentDirectory()), "appsettings.json", true, true);

            _config = configurationBuilder.Build();
        }
    }
}