using MediatR;

namespace Net6WebApiTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Trn { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string Parish { get; set; }
    }
}