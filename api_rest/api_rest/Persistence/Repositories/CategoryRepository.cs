using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api_rest.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.Include(x => x.Products).ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
           _context.Categories.Update(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
