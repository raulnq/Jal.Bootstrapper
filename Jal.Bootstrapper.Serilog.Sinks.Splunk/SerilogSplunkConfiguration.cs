namespace Jal.Bootstrapper.Serilog.Sinks.Splunk
{
    public class SerilogSplunkConfiguration
    {
        public string Url { get; set; }

        public string Token { get; set; }

        public string Index { get; set; }

        public int BatchSizeLimit { get; set; }

        public int BatchIntervalInSeconds { get; set; }

        public string Source { get; set; }

        public string SourceType { get; set; }

        public bool Rendertemplate { get; set; }
    }
}