using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Net6WebApiTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}