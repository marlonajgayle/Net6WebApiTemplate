namespace Net6WebApiTemplate.Application.HealthChecks
{
    public class HealthCheckResponse
    {
        public string OverallStatus { get; set; }
        public IEnumerable<HealthCheck> HealthChecks { get; set; }
        public string TotalDuration { get; set; }
    }
}