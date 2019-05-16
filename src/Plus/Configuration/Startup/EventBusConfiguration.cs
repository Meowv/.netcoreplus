namespace Plus.Configuration.Startup
{
    /// <summary>
    /// EventBusConfiguration
    /// </summary>
    internal class EventBusConfiguration : IEventBusConfiguration
    {
        public bool UseDefaultEventBus { get; set; }

        public EventBusConfiguration()
        {
            UseDefaultEventBus = true;
        }
    }
}