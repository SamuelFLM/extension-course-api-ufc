using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace api_rest.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryEntityConfiguration());
            builder.ApplyConfiguration(new ProductEntityConfiguration());
        }
    }
}
