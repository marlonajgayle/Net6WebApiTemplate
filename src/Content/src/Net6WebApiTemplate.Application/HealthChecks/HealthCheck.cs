namespace Net6WebApiTemplate.Application.HealthChecks
{
    public class HealthCheck
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}