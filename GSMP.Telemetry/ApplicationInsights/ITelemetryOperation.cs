using System;
using System.Collections.Generic;

namespace GSMP.Telemetry.ApplicationInsights
{
    public interface ITelemetryOperation
    {
        /// <summary>
        /// Gets or sets Operation ID.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets whether operation has finished successfully.
        /// </summary>
        bool? Success { get; set; }

        /// <summary>
        /// Gets or sets the duration of the operation.
        /// </summary>
        TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the timestamp for the operation.
        /// </summary>
        DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the value that defines absolute order of the telemetry item.
        /// </summary>
        string Sequence { get; set; }

        /// <summary>
        /// Gets the custom metrics collection.
        /// </summary>
        IDictionary<string, double> Metrics { get; }

        /// <summary>
        /// Gets the custom properties collection.
        /// </summary>
        IDictionary<string, string> Properties { get; }
    }
}