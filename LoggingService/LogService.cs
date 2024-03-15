using Newtonsoft.Json;
using Service.Enums;
using Service.Globals;
using Service.Packets;

namespace Service.Logging
{
    public class LogService : MqttService
    {
        public override string Name => Services.Logger;

        private LogLevel Threshold => ((LoggerConfig)Config).LogThreshold;

        private string LogFolder => ((LoggerConfig)Config).LogFolder;

        public LogService(LoggerConfig config) : base(config)
        {
            Subscribe(LogPacket.Topic, HandleLogMessage);
        }

        private void HandleLogMessage(string message)
        {
            try
            {
                var packet = JsonConvert.DeserializeObject<LogPacket>(message);
                if (packet.Level >= Threshold)
                {
                    string logMsg = $"{DateTime.Now:G}: {packet.Level} - {packet.Message}{Environment.NewLine}";
                    if (!string.IsNullOrEmpty(packet.StackTrace))
                    {
                        logMsg += $"{packet.StackTrace}{Environment.NewLine}";
                    }

                    LogMessage(logMsg);
                }
            }
            catch (Exception e)
            {
                LogMessage($"{DateTime.Now:G}: {LogLevel.Error} - {e.Message}{Environment.NewLine}{e.StackTrace}{Environment.NewLine}");
            }
        }

        private void LogMessage(string message)
        {
            Directory.CreateDirectory(LogFolder);
            string logFilePath = Path.Combine(LogFolder, $"{DateTime.Now:yyyyMMdd}.log");
            File.AppendAllText(logFilePath, message);
        }
    }
}
