using Plus.Configuration.Startup;

namespace Plus.AutoMapper
{
    /// <summary>
    /// PlusAutoMapperConfigurationExtensions
    /// </summary>
    public static class PlusAutoMapperConfigurationExtensions
    {
        public static IPlusAutoMapperConfiguration PlusAutoMapper(this IPlusStartupConfiguration configuration)
        {
            return configuration.Get<IPlusAutoMapperConfiguration>();
        }
    }
}