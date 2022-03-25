using System;

namespace GSMP.Telemetry.GoogleAnalytics
{
    public class ClientOptions
    {
        public Uri BaseUrl { get; set; } = new Uri("https://www.google-analytics.com/");
        public string CollectUrl { get; set; } = "/collect";
        public string BatchUrl { get; set; } = "/batch";
        public string TestUrl { get; set; } = "/debug/collect";
        public string TrackingId { get; set; }

        public string DataSource { get; set; } = "AUTH";
        public string ApplicationName { get; set; } = "MG.Authentication";
    }
}
