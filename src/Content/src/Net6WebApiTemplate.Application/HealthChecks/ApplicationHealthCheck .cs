using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace Net6WebApiTemplate.Application.HealthChecks
{
    public class ApplicationHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var assembly = Assembly.Load("Net6WebApiTemplate.Api");
            var versionNumber = assembly.GetName().Version;

            return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
        }
    }
}