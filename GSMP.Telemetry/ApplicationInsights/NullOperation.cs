using System;
using System.Collections.Generic;

namespace GSMP.Telemetry.ApplicationInsights
{
    public class NullOperation : ITelemetryOperation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool? Success { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Sequence { get; set; }

        public IDictionary<string, double> Metrics { get; set; }
        public IDictionary<string, string> Properties { get; set; }
    }
}