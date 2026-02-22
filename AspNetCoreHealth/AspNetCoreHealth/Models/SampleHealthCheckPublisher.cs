using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreHealth.Models
{
    public class SampleHealthCheckPublisher : IHealthCheckPublisher
    {
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Healthy)
            {
                // ...
                foreach (var entry in report.Entries)
                {
                    Console.WriteLine($"{entry.Key} - {entry.Value.Status}");
                }
            }
            else
            {
                // ...
            }

            return Task.CompletedTask;
        }
    }
}
