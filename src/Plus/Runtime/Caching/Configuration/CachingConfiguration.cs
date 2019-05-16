using Plus.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Plus.Runtime.Caching.Configuration
{
    /// <summary>
    /// CachingConfiguration
    /// </summary>
    internal class CachingConfiguration : ICachingConfiguration
    {
        public IPlusStartupConfiguration PlusConfiguration { get; private set; }

        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration(IPlusStartupConfiguration plusConfiguration)
        {
            PlusConfiguration = plusConfiguration;

            _configurators = new List<ICacheConfigurator>();
        }

        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}