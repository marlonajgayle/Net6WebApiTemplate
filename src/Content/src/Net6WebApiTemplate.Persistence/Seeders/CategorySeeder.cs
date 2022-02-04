using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Persistence.Seeders
{
    public static class CategorySeeder
    {
        public static async Task Initialize(INet6WebApiTemplateDbContext context)
        {
            await SeedOne("TestCategory1", "Description 1 ", context, new CancellationToken());
            await SeedOne("TestCategory2", "Description 2 ", context, new CancellationToken());
            await SeedOne("TestCategory3", "Description 3 ", context, new CancellationToken());
            await SeedOne("TestCategory4", "Description 4 ", context, new CancellationToken());
            await SeedOne("TestCategory5", "Description 5 ", context, new CancellationToken());
        }

        private static async Task SeedOne(string CategoryName, string description, INet6WebApiTemplateDbContext context, CancellationToken cancellationToken)
        {
            await EnsureCategory(CategoryName, description, context, cancellationToken);
        }

        private static async Task EnsureCategory(string CategoryName, string description, INet6WebApiTemplateDbContext context, CancellationToken cancellationToken)
        {
            Category Category = await context.Categories.Where(s => s.CategoryName == CategoryName).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (Category == null)
            {
                Category = new Category
                {
                    CategoryName = CategoryName,
                    Description = description
                };
                context.Categories.Add(Category);
            }
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}