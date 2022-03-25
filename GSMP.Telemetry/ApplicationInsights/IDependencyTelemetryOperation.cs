namespace GSMP.Telemetry.ApplicationInsights
{
    public interface IDependencyTelemetryOperation : ITelemetryOperation
    {
        /// <summary>
        /// Gets or sets the Result Code.
        /// </summary>
        string ResultCode { get; set; }

        /// <summary>
        /// Gets or sets data associated with the current dependency instance. Command name/statement for SQL dependency, URL for http dependency.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        /// Gets or sets target of dependency call. SQL server name, url host, etc.
        /// </summary>
        string Target { get; set; }

        /// <summary>
        /// Gets or sets the dependency type name.
        /// </summary>
        string Type { get; set; }
    }
}