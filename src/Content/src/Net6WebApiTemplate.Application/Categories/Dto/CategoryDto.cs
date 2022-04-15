using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Application.Categories.Dto
{
    public class CategoryDto
    {
        public int Id{ get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product>? Products { get; private set; }
    }
}
