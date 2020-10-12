﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Jal.Bootstrapper.Interface;
using Serilog;
using Serilog.Sinks.Splunk;

namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class SerilogSplunkBootstrapper : IBootstrapper<LoggerConfiguration>
    {
        private readonly Action<LoggerConfiguration> _action;

        private readonly SerilogSplunkConfiguration _configuration;

        public SerilogSplunkBootstrapper(SerilogSplunkConfiguration configuration, Action<LoggerConfiguration> action = null)
        {
            _action = action;

            _configuration = configuration;
        }

        public void Run()
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.EventCollector(
                    _configuration.Url,
                    _configuration.Token, batchSizeLimit: _configuration.BatchSizeLimit, queueLimit: _configuration.QueueLimit,
                    batchIntervalInSeconds: _configuration.BatchIntervalInSeconds,
                    jsonFormatter: new SplunkJsonFormatter(_configuration.Rendertemplate, null, _configuration.Source, _configuration.SourceType, GetLocalIpAddress(), _configuration.Index));


            _action?.Invoke(loggerConfiguration);

            Log.Logger = loggerConfiguration.CreateLogger();

            Result = loggerConfiguration;
        }

        public LoggerConfiguration Result { get; set; }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList
                .FirstOrDefault(add => add.AddressFamily == AddressFamily.InterNetwork)
                ?.ToString();
        }
    }
}