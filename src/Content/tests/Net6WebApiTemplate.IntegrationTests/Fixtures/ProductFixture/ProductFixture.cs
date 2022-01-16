using Microsoft.Extensions.DependencyInjection;
using Net6WebApiTemplate.Domain.Entities;
using Net6WebApiTemplate.Persistence;
using System;
namespace Net6WebApiTemplate.IntegrationTests.Fixtures;


public partial class IntegrationTestSuiteFixture : IDisposable
{   
    public void ensureProduct()
    {
        using (var context = this.ServiceProvider.GetService<Net6WebApiTemplateDbContext>())
        {
            var product = new Product 
            {
                ProductName = "TestItem",
                UnitPrice = 1.0m,
                CategoryId = 1
            };
            context.Products.Add(product);         
            context.SaveChanges();
        }
    }
 
}