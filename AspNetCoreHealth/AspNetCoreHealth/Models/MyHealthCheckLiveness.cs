using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreHealth.Models
{
    public class MyHealthCheckLiveness : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (StartupWork.DoWork())
            {
                return Task.FromResult(HealthCheckResult.Healthy("The startup task has completed."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("That startup task is still running."));
        }
    }
}
