using System;
using System.Collections.Generic;

namespace GSMP.Telemetry.ApplicationInsights
{
    public class NullTelemetryOperationStarter : ITelemetryOperationStarter
    {
        public bool IsEnabled()
        {
            return false;
        }

        public TelemetryOperationHolder<TOperation> StartOperation<TOperation>(string operationName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
            where TOperation : ITelemetryOperation
        {
            return new TelemetryOperationHolder<TOperation>((TOperation)(ITelemetryOperation)new NullOperation(), _disposable);
        }

        public TelemetryOperationHolder<IDependencyTelemetryOperation> StartDependency(string operationName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            return new TelemetryOperationHolder<IDependencyTelemetryOperation>(new NullDependencyTelemetryOperation(), _disposable);
        }

        public void StopOperation<TOperation>(TelemetryOperationHolder<TOperation> operation)
            where TOperation : ITelemetryOperation
        {
        }

        private static IDisposable _disposable = new Disposable();

        private class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}