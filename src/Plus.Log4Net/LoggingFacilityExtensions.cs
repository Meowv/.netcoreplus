using Castle.Facilities.Logging;

namespace Plus.Log4Net
{
    /// <summary>
    /// LoggingFacilityExtensions
    /// </summary>
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseLog4Net(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<Log4NetLoggerFactory>();
        }
    }
}