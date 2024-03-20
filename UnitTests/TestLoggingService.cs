using Service.Logging;
using Service.Configuration;
using Service.Enums;

namespace UnitTests
{
    public class TestLoggingService
    {
        private const string LOGS_FOLDER = "logs";

        private const string CONFIG_FILE = "config.json";

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists(LOGS_FOLDER))
            {
                Directory.Delete(LOGS_FOLDER, true);
            }

            if (File.Exists(CONFIG_FILE))
            {
                File.Delete(CONFIG_FILE);
            }
        }

        [Test]
        [TestCase(LogLevel.Debug, "Debug Test Message", "Debug Test Stacktrace")]
        [TestCase(LogLevel.Info, "Info Test Message", "Info Test Stacktrace")]
        [TestCase(LogLevel.Warning, "Warning Test Message", "Warning Test Stacktrace")]
        [TestCase(LogLevel.Error, "Error Test Message", "Error Test Stacktrace")]
        [TestCase(LogLevel.Fatal, "Fatal Test Message", "Fatal Test Stacktrace")]
        public void TestLogging(LogLevel level, string message, string stacktrace)
        {
            var service = new LogService(Config.Load<LoggerConfig>(CONFIG_FILE));
            service.Log(level, message, stacktrace);
            Thread.Sleep(1000);
            ValidateLogFile(level, message, stacktrace);
        }

        [Test]
        [TestCase(LogLevel.Debug, "Debug Test Message", "Debug Test Stacktrace")]
        [TestCase(LogLevel.Info, "Info Test Message", "Info Test Stacktrace")]
        [TestCase(LogLevel.Warning, "Warning Test Message", "Warning Test Stacktrace")]
        [TestCase(LogLevel.Error, "Error Test Message", "Error Test Stacktrace")]
        [TestCase(LogLevel.Fatal, "Fatal Test Message", "Fatal Test Stacktrace")]
        public void TestLoggingThresholds(LogLevel level, string message, string stacktrace)
        {
            var config = Config.Load<LoggerConfig>(CONFIG_FILE);
            config.LogThreshold = LogLevel.Warning;
            var service = new LogService(config);
            service.Log(level, message, stacktrace);
            Thread.Sleep(1000);

            if (level >= LogLevel.Warning)
            {
                ValidateLogFile(level, message, stacktrace);
            }
            else
            {
                Assert.False(Directory.Exists(LOGS_FOLDER));
            }
        }

        private static void ValidateLogFile(LogLevel level, string message, string stacktrace)
        {
            var logFiles = Directory.GetFiles(LOGS_FOLDER);
            Assert.AreEqual(1, logFiles.Length);
            var logFile = logFiles[0];
            var logContent = File.ReadAllText(logFile);
            Assert.IsTrue(logContent.Contains(level.ToString()));
            Assert.IsTrue(logContent.Contains(message));
            Assert.IsTrue(logContent.Contains(stacktrace));
        }
    }
}
