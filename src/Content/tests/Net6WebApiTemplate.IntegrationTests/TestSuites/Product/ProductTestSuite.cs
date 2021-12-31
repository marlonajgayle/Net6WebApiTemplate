using Net6WebApiTemplate.IntegrationTests.Factory;
using Net6WebApiTemplate.IntegrationTests.Fixtures;
using System.Net.Http;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{    
    public partial class ProductTestSuite : IClassFixture<CustomWebApplicationFactory<Net6WebApiTemplate.A>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Api.Startup> _factory;
        private readonly IntegrationTestSuiteFixture _fixture;

        public ProductTestSuite(CustomWebApplicationFactory<Api.Startup> factory, IntegrationTestSuiteFixture fixture)
        {
            // Arrange
            _client = factory.CreateClient();
            _factory = factory;
            _fixture = fixture;
        }
    }
}
