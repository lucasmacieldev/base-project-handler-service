using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.HealthChecks
{
    public class HealthCheckPrometheusPublisher : IHealthCheckPublisher
    {
        private static string[] _healthcheckLabels = new string[1]{ "healthcheck"  };
        public HealthCheckPrometheusPublisher() { }
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            foreach (var entry in report.Entries)
                SetHealthCheckStatus(entry.Key, (int)entry.Value.Status);

            return Task.CompletedTask;
        }

        private static void SetHealthCheckStatus(string healthcheck, int status)
        {
            var gauge = Prometheus.Metrics.CreateGauge("healthcheck", "", _healthcheckLabels);
            var values = new string[1] { healthcheck };
            var childMetric = gauge.Labels(values);
            childMetric.Set(status);
        }
    }
}
