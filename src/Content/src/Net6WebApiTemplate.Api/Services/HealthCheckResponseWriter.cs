using Microsoft.Extensions.Diagnostics.HealthChecks;
using Net6WebApiTemplate.Application.HealthChecks;
using Newtonsoft.Json;

namespace Net6WebApiTemplate.Api.Services
{
    public static class HealthCheckResponseWriter
    {
        public static async Task WriteHealthCheckResponse(HttpContext httpContext, HealthReport report)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new HealthCheckResponse()
            {
                OverallStatus = report.Status.ToString(),
                TotalDuration = report.TotalDuration.TotalSeconds.ToString("0:0.00"),
                HealthChecks = report.Entries.Select(x => new HealthCheck
                {
                    Status = x.Value.Status.ToString(),
                    Component = x.Key,
                    Description = x.Value.Description == null ? "" : x.Value.Description,
                    Duration = x.Value.Duration.TotalSeconds.ToString("0:0.00")
                }),

            };

            await httpContext.Response.WriteAsync(text: JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}
