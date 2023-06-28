using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.HealthChecks
{
    public class HealthCheckFilePublisher : IHealthCheckPublisher
    {
        public HealthCheckFilePublisher() { }
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Healthy)
            {
                string fullPathName = "./kicking.txt";
                if (!System.IO.File.Exists(fullPathName))
                    System.IO.File.Create(fullPathName);
                else
                    System.IO.File.SetLastWriteTimeUtc(fullPathName, DateTime.UtcNow);
            }

            return Task.CompletedTask;
        }
    }
}
