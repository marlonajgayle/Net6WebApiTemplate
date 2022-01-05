using Microsoft.VisualStudio.TestPlatform.TestHost;
using Net6WebApiTemplate.IntegrationTests.Factory;
using Net6WebApiTemplate.IntegrationTests.Fixtures;
using System.Net.Http;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{    
    public partial class ProductTestSuite : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly IntegrationTestSuiteFixture _fixture;

        public ProductTestSuite(CustomWebApplicationFactory<Program> factory, IntegrationTestSuiteFixture fixture)
        {
            // Arrange
            _client = factory.CreateClient();
            _factory = factory;
            _fixture = fixture;
        }
    }
}
