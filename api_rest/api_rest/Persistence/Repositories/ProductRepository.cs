using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api_rest.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task PostAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
