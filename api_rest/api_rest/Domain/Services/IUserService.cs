using api_rest.Communication;
using api_rest.Domain.Models;

namespace api_rest.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> AuthLoginAsync(User user);
        Task<UserResponse> PostAsync(User user);
    }
}
