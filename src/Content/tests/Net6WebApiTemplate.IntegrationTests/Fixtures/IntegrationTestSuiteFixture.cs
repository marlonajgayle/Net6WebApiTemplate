using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Persistence;
using System;
using System.IO;

namespace Net6WebApiTemplate.IntegrationTests.Fixtures
{
    public partial class IntegrationTestSuiteFixture : IDisposable
    {

        public const string TestProductName = "TestProduct1";
        public const string TestCategoryName = "TestCategory1";
        public static int TestProductId = 0;
        public static int TestCategoryId = 0;

        public ServiceProvider ServiceProvider { get; private set; }

        public IntegrationTestSuiteFixture()
        {
            var appSettings = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.Test.json")
            .Build();

            var rootAppSettings = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json")
            .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<Net6WebApiTemplateDbContext>(options => options.UseSqlServer(appSettings["ConnectionStrings:Net6WebApiConnection"]),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
      

        public void Dispose()
        {           
        }      
    }
}