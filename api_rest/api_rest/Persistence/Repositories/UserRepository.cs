using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api_rest.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AuthLoginAsync(User user)
        {
            var response = await _context.Users.FirstOrDefaultAsync(x => x.Username == user.Username.Trim() && x.Password == user.Password.Trim());

            if (response is null)
                return false;

            return true;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task PostAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
