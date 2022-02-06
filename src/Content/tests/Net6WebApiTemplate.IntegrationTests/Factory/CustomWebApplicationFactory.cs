using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Persistence;

namespace Net6WebApiTemplate.IntegrationTests.Factory
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                    .ConfigureServices(services =>
                    {                      
                        services.AddDbContext<Net6WebApiTemplateDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });
                    });
        }
    }
}