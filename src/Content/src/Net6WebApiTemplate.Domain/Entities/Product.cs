namespace Net6WebApiTemplate.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; } 
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}