using System;
using Jal.Bootstrapper.Interface;
using Serilog;

namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class SerilogSplunkBootstrapper : IBootstrapper<LoggerConfiguration>
    {
        private readonly Action<LoggerConfiguration> _loggerSetup;

        private readonly string _context;

        private readonly int _batchSizeLimit;

        private readonly TimeSpan _batchInterval;

        public SerilogSplunkBootstrapper(string context, int batchSizeLimit, TimeSpan batchInterval, Action<LoggerConfiguration> loggerSetup = null)
        {
            _loggerSetup = loggerSetup;

            _context = context;

            _batchSizeLimit = batchSizeLimit;

            _batchInterval = batchInterval;
        }

        public void Configure()
        {
            var loggerSettings = new LoggerSettings(_context, _batchSizeLimit, _batchInterval);

            var loggerConfiguration = new LoggerConfiguration().ReadFrom.Settings(loggerSettings);

            if (_loggerSetup!=null)
            {
                _loggerSetup(loggerConfiguration);
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            Result = loggerConfiguration;
        }

        public LoggerConfiguration Result { get; set; }
    }
}