namespace GSMP.Telemetry.GoogleAnalytics
{
    public abstract class BaseRequest
    {
        public abstract string HitType { get; }

        public string ClientId { get; set; }
        public string UserId { get; set; }

        public string DocumentReferrer { get; set; }
        public string DocumentHostName { get; set; }
        public string DocumentPath { get; set; }

        public string ApplicationId { get; set; }

        public string UserIp { get; set; }
        public string UserAgent { get; set; }
    }
}
