using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Domain.Entities;
using Net6WebApiTemplate.Persistence;
using System;
using System.Linq;

namespace Net6WebApiTemplate.IntegrationTests.Fixtures;


public partial class IntegrationTestSuiteFixture : IDisposable
{


    public Product GetProduct(string productName)
    {
        using (var context = this.ServiceProvider.GetService<Net6WebApiTemplateDbContext>())
        {
            return context.Products
                .Where(s => s.ProductName == productName).FirstOrDefault();
        }
    }

    public void ensureProduct()
    {
        using (var context = this.ServiceProvider.GetService<Net6WebApiTemplateDbContext>())
        {
            Product product = context.Products
             .Where(s => s.ProductName == IntegrationTestSuiteFixture.TestProductName).FirstOrDefault();

            if (product == null)
            {
                 product = new Product
                {
                    ProductName = "TestProduct1",
                    UnitPrice = 1.0m,
                    CategoryId = 1
                };
                context.Products.Add(product);
                context.SaveChanges();
            }
            IntegrationTestSuiteFixture.TestProductId = product.Id;         
        }
    }
 
}