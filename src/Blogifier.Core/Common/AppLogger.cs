using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.Common
{
    public static class AppLogger
    {
        private static Serilog.Core.Logger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
        }

        public static void LogInformation(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null)
            {
                _logger.Information("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }

        public static void LogWarning(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null)
            {
                _logger.Warning("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }

        public static void LogError(string msg)
        {
            var _logger = GetLogger();

            if (_logger != null)
            {
                _logger.Error("[BLOGIFIER] " + msg);
            }

            _logger.Dispose();
        }
    }
}
