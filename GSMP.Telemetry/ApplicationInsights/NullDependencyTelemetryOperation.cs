namespace GSMP.Telemetry.ApplicationInsights
{
    public class NullDependencyTelemetryOperation : NullOperation, IDependencyTelemetryOperation
    {
        public string ResultCode { get; set; }
        public string Data { get; set; }
        public string Target { get; set; }
        public string Type { get; set; }
    }
}