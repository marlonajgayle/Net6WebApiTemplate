using Net6WebApiTemplate.IntegrationTests.Factory;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{    
    public partial class ProductTestSuite : IClassFixture<CustomWebApplicationFactory<Program>>
    {   
        [Fact]
        public async Task When_DeleteProduct_With_ValidClient_And_ValidRequestBody_Then_Success()
        {
            var response = await _client.DeleteAsync("api/v1/products/11");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
