using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Configuration;
using Serilog.Sinks.Splunk;
using Splunk.Client;

namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class LoggerSettings : ILoggerSettings
    {
        private readonly string _context;

        private readonly int _batchSizeLimit;

        private readonly TimeSpan _batchInterval;

        public LoggerSettings(string context, int batchSizeLimit, TimeSpan batchInterval)
        {
            _context = context;

            _batchSizeLimit = batchSizeLimit;

            _batchInterval = batchInterval;
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            var context = JsonConvert.DeserializeObject<SplunkContext>(_context, new SpecificTypeConverter<SplunkContext>(CreateSplunkContext));

            loggerConfiguration.WriteTo.SplunkViaHttp(context, _batchSizeLimit, _batchInterval);
        }

        private SplunkContext CreateSplunkContext(JObject j)
        {
            return new SplunkContext(
                new Context(
                    (Scheme)Enum.Parse(typeof(Scheme), j.Value<string>("scheme")),
                    j.Value<string>("host"),
                    j.Value<int>("port")),
                j.Value<string>("index"),
                j.Value<string>("username"),
                j.Value<string>("password"),
                transmitterArgs: new TransmitterArgs
                {
                    Source = j["transmitterArgs"].Value<string>("source"),
                    SourceType = j["transmitterArgs"].Value<string>("sourceType")
                });
        }
    }
}
