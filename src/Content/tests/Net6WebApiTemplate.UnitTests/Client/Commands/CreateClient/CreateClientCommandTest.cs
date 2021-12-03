using Net6WebApiTemplate.Application.Clients.Commands.CreateClient;
using Net6WebApiTemplate.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Net6WebApiTemplate.UnitTests.Client.Commands.CreateClient
{
    public class CreateClientCommandTest : TestBase
    {
        [Fact]
        public void Should_AddNewClient_When_RequestValid()
        {
            // Arrange
            var sut = new CreateClientCommandHandler(_context);
            var newClient = new CreateClientCommand
            {
                FirstName = "John",
                MiddleName = "Jacob",
                LastName = "Doe",
                Trn = "123456789",
                AddressLine1 = "1 Main Street",
                AddressLine2 = "Kingston 10",
                Parish = "Kingston"
            };

            // Act
            var response = sut.Handle(newClient, CancellationToken.None);

            // Assert
            Assert.True(response.IsCompleted);
        }
    }
}