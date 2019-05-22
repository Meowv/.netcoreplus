using Plus.Configuration.Startup;
using Plus.Dependency;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// DefaultConnectionStringResolver
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        private readonly IPlusStartupConfiguration _configuration;

        public DefaultConnectionStringResolver(IPlusStartupConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            string defaultNameOrConnectionString = _configuration.DefaultSettings.DefaultNameOrConnectionString;
            if (!string.IsNullOrWhiteSpace(defaultNameOrConnectionString))
            {
                return defaultNameOrConnectionString;
            }
            return defaultNameOrConnectionString;
        }
    }
}