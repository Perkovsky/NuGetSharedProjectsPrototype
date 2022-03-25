using System;

namespace GSMP.Telemetry.ApplicationInsights
{
    /// <summary>
    /// Represents the operation item that holds telemetry which is tracked on end request. Operation can be associated with either WEB or SQL dependencies.
    /// </summary>
    public class TelemetryOperationHolder<TOperation> : IDisposable
        where TOperation : ITelemetryOperation
    {
        /// <summary>
        /// Gets Telemetry item of interest that is created when StartOperation function of ClientExtensions is invoked.
        /// </summary>
        public TOperation Telemetry { get; }

        public IDisposable OperationHolder { get; }

        public bool IsDisposed { get; private set; }

        public bool IsNotDisposed => !IsDisposed;

        public TelemetryOperationHolder(TOperation telemetry, IDisposable operationHolder)
        {
            Telemetry = telemetry;
            OperationHolder = operationHolder;
        }

        /// <summary>
        /// Dispose method to clear the variables.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Computes the duration and tracks the respective telemetry item on dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            OperationHolder?.Dispose();
            IsDisposed = true;
        }
    }
}