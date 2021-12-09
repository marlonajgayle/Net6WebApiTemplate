using Net6WebApiTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6WebApiTemplate.Application.Products.NQueries.GetProducts
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
