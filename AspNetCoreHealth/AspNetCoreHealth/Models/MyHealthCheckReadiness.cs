using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCoreHealth.Models
{
    public class MyHealthCheckReadiness : IHealthCheck
    {
        private readonly string _connectionString;

        public MyHealthCheckReadiness(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_connectionString);

                sqlConnection.OpenAsync(cancellationToken);

                using var command = sqlConnection.CreateCommand();
                command.CommandText = "SELECT 1";

                command.ExecuteScalarAsync(cancellationToken);

                return Task.FromResult(HealthCheckResult.Healthy("The Database is working properly"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }
        }
    }
}
