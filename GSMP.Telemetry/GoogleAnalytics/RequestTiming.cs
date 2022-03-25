namespace GSMP.Telemetry.GoogleAnalytics
{
    public class RequestTiming : BaseRequest
    {
        public override string HitType => "timing";

        public string Category { get; set; } = "AUTH";
        public string Variable { get; set; } = "CallDuration";
        public string Label { get; set; }
        public long Value { get; set; }

        public string HttpMethod { get; set; }
    }
}
