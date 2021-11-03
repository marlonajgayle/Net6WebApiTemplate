namespace Net6WebApiTemplate.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<Product> Products { get; private set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}