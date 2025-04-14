using api_rest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_rest.Persistence.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();
            builder
                .Property(p => p.Name)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasMany(p => p.Products)
                .WithOne()
                .HasForeignKey(p => p.CategoryId);


            builder.HasData(
                new Category { Id = 1, Name = "Fruits and Vegetables" },
                new Category { Id = 2, Name = "Breads" }
            );
        }
    }
}
