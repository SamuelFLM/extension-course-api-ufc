using api_rest.Domain.Models;

namespace api_rest.Domain.Repositories
{
    public interface IUserRepository
    {
        Task PostAsync(User user);
        Task<IEnumerable<User>> GetAsync();
        Task<bool> AuthLoginAsync(User user);
    }
}
