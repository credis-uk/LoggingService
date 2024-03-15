using Service.Configuration;
using Service.Enums;

namespace Service.Logging
{
    public class LoggerConfig : Config
    {
        public string LogFolder { get; set; } = "logs";

        public LogLevel LogThreshold { get; set; } = LogLevel.Debug;
    }
}
