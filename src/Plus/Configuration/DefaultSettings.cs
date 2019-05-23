namespace Plus.Configuration
{
    public class DefaultSettings : SettingsBase
    {
        public string GetDefaultNameOrConnectionString()
        {
            return Config["DefaultNameOrConnectionString"] ?? "";
        }
    }
}