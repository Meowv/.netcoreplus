namespace Plus.Configuration
{
    public class DefaultSettings : SettingsBase
    {
        public string DefaultNameOrConnectionString => Config["ConnectionStrings"];
    }
}