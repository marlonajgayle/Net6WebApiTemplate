using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Application.Common.Behaviours;
using Net6WebApiTemplate.Application.HealthChecks;
using System.Reflection;

namespace Net6WebApiTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            // Register Application Health Checks
            services.AddHealthChecks()
                .AddCheck<ApplicationHealthCheck>(name: "Net6WebApiTemplate API");

            // Register Fluent Validation service
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register MediatR Services
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}