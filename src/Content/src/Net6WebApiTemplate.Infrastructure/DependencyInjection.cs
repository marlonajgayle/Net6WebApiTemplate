using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Net6WebApiTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            return services;
        }
    }
}