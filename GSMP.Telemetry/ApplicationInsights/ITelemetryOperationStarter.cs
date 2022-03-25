using System.Collections.Generic;

namespace GSMP.Telemetry.ApplicationInsights
{
    public interface ITelemetryOperationStarter : ITelemetryService
    {
        /// <summary>
        /// Start operation creates an operation object with a telemetry item.
        /// </summary>
        /// <param name="operationName">Name of the operation that customer is planning to propagate.</param>
        /// <param name="properties">Dictionary of application-defined property names and values providing additional information about this operation</param>
        /// <param name="metrics">Dictionary of application-defined operation metrics</param>
        /// <returns>Operation item object with a new telemetry item having current start time and timestamp.</returns>
        TelemetryOperationHolder<TOperation> StartOperation<TOperation>(string operationName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
            where TOperation : ITelemetryOperation;

        /// <summary>
        /// Start operation creates an operation object with a dependency telemetry item.
        /// </summary>
        /// <param name="operationName">Name of the operation that customer is planning to propagate.</param>
        /// <param name="properties">Dictionary of application-defined property names and values providing additional information about this operation</param>
        /// <param name="metrics">Dictionary of application-defined operation metrics</param>
        /// <returns>Operation item object with a new telemetry item having current start time and timestamp.</returns>
        TelemetryOperationHolder<IDependencyTelemetryOperation> StartDependency(string operationName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        /// <summary>
        /// Stop operation computes the duration of the operation and tracks it using the respective telemetry client.
        /// </summary>
        /// <param name="operation">Operation object to compute duration and track.</param>
        void StopOperation<TOperation>(TelemetryOperationHolder<TOperation> operation)
            where TOperation : ITelemetryOperation;
    }
}