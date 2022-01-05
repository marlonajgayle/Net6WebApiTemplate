using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Persistence;
using System;

namespace Net6WebApiTemplate.UnitTests.Common
{
    public class Net6WebApiTemplateDbContextFactory
    {
        public static Net6WebApiTemplateDbContext Create()
        {
            var options = new DbContextOptionsBuilder<Net6WebApiTemplateDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new Net6WebApiTemplateDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(Net6WebApiTemplateDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}