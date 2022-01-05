using Microsoft.VisualStudio.TestPlatform.TestHost;
using Net6WebApiTemplate.IntegrationTests.Factory;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.TestSuites.Product
{
    public partial class PostProductTestSuite : IClassFixture<CustomWebApplicationFactory<Program>>
    {
    }
}
