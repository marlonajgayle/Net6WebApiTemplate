using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.IntegrationTests.Factory;
using Net6WebApiTemplate.IntegrationTests.Fixtures;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{    
    public partial class ProductTestSuite : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Fact]
        public async Task When_GetProductById_With_ValidClient_And_ValidRequestBody_Then_Success()
        {
            _fixture.ensureProduct();

            var response = await _client.GetAsync($"api/v1/products/{IntegrationTestSuiteFixture.TestProductId}");
            string responseString = await response.Content.ReadAsStringAsync();
            ProductDto result = JsonConvert.DeserializeObject<ProductDto>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.IsType<ProductDto>(result);
        }
    }
}
