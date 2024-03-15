using Service.Logging;
using Service;
using Service.Configuration;

ServiceFactory.ServiceRunner<LogService>(Config.Load<LoggerConfig>("config.json"));