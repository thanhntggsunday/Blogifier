using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace Blogifier.Core.Common
{
    public static class Logger
    {
        private static Serilog.Core.Logger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, shared:true)
                .CreateLogger();
        }

        public static void LogInformation(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null && ApplicationSettings.EnableLogging)
            {
                _logger.Information("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }

        public static void LogWarning(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null && ApplicationSettings.EnableLogging)
            {
                _logger.Warning("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }

        public static void LogError(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null && ApplicationSettings.EnableLogging)
            {
                _logger.Error("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }
    }
}
