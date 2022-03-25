namespace GSMP.Telemetry.ApplicationInsights
{
    public interface ITelemetryService
    {
        /// <summary>
        /// Check to determine if the tracking is enabled.
        /// </summary>
        bool IsEnabled();
    }
}