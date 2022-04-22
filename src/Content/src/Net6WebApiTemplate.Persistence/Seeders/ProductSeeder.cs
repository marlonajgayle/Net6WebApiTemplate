using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Persistence.Seeders
{
    public static class ProductSeeder
    {
        public static async Task Initialize(INet6WebApiTemplateDbContext context)
        {
            await SeedOne("TestProduct1", 10.0m, 1, context, new CancellationToken());           
            await SeedOne("TestProduct2", 10.0m, 1, context, new CancellationToken());            
            await SeedOne("TestProduct3", 10.0m, 1, context, new CancellationToken());            
            await SeedOne("TestProduct4", 10.0m, 1, context, new CancellationToken());            
            await SeedOne("TestProduct5", 10.0m, 1, context, new CancellationToken());
        }

        private static async Task SeedOne(string productName, decimal unitPrice, int categoryId, INet6WebApiTemplateDbContext context, CancellationToken cancellationToken)
        {
            await EnsureProduct(productName, unitPrice, categoryId, context, cancellationToken);
        }

        private static async Task EnsureProduct(string productName, decimal unitPrice, int categoryId, INet6WebApiTemplateDbContext context, CancellationToken cancellationToken)
        {
            Product product = await context.Products.Where(s => s.ProductName == productName).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (product == null)
            {
                product = new Product
                {
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    CategoryId = categoryId             
                };               
                context.Products.Add(product);
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}