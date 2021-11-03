using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Net6WebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR 
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}