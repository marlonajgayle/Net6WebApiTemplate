using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Persistence.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("CategoryId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder
            .HasMany<Product>(product => product.Products)
            .WithOne(category => category.Category);
        }
    }
}