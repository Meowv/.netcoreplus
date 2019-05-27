namespace Plus.Configuration
{
    /// <summary>
    /// DefaultSettings
    /// </summary>
    public class DefaultSettings : SettingsBase
    {
        /// <summary>
        /// GetDefaultNameOrConnectionString
        /// </summary>
        /// <returns></returns>
        public string GetDefaultNameOrConnectionString()
        {
            return Config["DefaultNameOrConnectionString"];
        }
    }
}