using Microsoft.VisualStudio.TestPlatform.TestHost;
using Net6WebApiTemplate.Api.Contracts.Version1.Requests;
using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.IntegrationTests.Factory;
using Newtonsoft.Json;
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
        public async Task When_PatchProduct_With_ValidClient_And_ValidRequestBody_Then_Success()
        {         
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, "api/v1/products/6");
            var pocoObject = new ProductRequest()
            {
                CategoryId = 1,
                ProductName = "Updated Test Product",
                UnitPrice = 11
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(pocoObject);

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.SendAsync(requestMessage);
            string responseString = await response.Content.ReadAsStringAsync();
            ProductDto? result = JsonConvert.DeserializeObject<ProductDto>(responseString);


            Assert.IsType<ProductDto>(result);
            Assert.Equal(pocoObject.ProductName, result?.ProductName);
            Assert.Equal(pocoObject.CategoryId, result?.CategoryId);
            Assert.Equal(pocoObject.UnitPrice, result?.UnitPrice);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}
