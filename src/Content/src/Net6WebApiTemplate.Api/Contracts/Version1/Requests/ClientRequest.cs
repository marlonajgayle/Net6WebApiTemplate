namespace Net6WebApiTemplate.Api.Contracts.Version1.Requests
{
    public class ClientRequest
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