using System;
using System.Collections.Generic;

namespace GSMP.Telemetry.ApplicationInsights
{
    public class NullTelemetryClientService : NullTelemetryOperationStarter, ITelemetryClientService
    {
        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
        }

        public void TrackTrace(string message, TelemetrySeverityLevel? severityLevel = null, IDictionary<string, string> properties = null)
        {
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
        }

        public void TrackException(Exception exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
        }

        public void TrackDependency(string dependencyTypeName, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, bool success)
        {
        }

        public void TrackDependency(string dependencyTypeName, string target, string dependencyName, string data, DateTimeOffset startTime, TimeSpan duration, string resultCode, bool success)
        {
        }

        public void TrackAvailability(string name, DateTimeOffset timeStamp, TimeSpan duration, string runLocation, bool success, string message = null, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
        }
    }
}