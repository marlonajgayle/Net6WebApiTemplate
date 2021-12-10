using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net6WebApiTemplate.Application.Common.Interfaces; 
using System;

namespace Net6WebApiTemplate.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<Net6WebApiTemplateDbContext>(name: "Application Database");

            services.AddDbContext<Net6WebApiTemplateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Net6WebApiConnection"),
                b => b.MigrationsAssembly(typeof(Net6WebApiTemplateDbContext).Assembly.FullName))
                .LogTo(Console.WriteLine, LogLevel.Information)); // disable for production;

            services.AddScoped<INet6WebApiTemplateDbContext>(provider =>
                provider.GetService<Net6WebApiTemplateDbContext>());

            return services;
        }
    }
}