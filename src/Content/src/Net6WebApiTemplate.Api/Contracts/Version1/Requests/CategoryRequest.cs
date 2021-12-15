using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Api.Contracts.Version1.Requests
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }        
    }
}