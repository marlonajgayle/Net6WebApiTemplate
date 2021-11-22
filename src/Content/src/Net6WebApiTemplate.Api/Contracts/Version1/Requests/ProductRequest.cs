using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Api.Contracts.Version1.Requests
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}