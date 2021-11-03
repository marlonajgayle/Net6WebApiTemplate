using Net6WebApiTemplate.Domain.ValueObjects;

namespace Net6WebApiTemplate.Domain.Entities
{
    public class Client
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Trn { get; set; }
        public Address Address { get; set; }
    }
}