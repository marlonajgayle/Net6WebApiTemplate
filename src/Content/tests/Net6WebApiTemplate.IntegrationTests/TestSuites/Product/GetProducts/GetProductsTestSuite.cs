using Microsoft.VisualStudio.TestPlatform.TestHost;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.IntegrationTests.Factory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{    
    public partial class ProductTestSuite : IClassFixture<CustomWebApplicationFactory<Program>>
    {   
        [Fact]
        public async Task When_GetProduct_With_ValidClient_And_ValidRequestBody_Then_Success()
        {
            
            var response = await _client.GetAsync("api/v1/products");
            string responseString = await response.Content.ReadAsStringAsync();
            List<ProductDto> result = JsonConvert.DeserializeObject<List<ProductDto>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.IsType<List<ProductDto>>(result);
        }
    }
}
