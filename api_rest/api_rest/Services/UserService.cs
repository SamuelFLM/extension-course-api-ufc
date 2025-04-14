using api_rest.Communication;
using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Domain.Services;

namespace api_rest.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnityOfWork _unityOfWork;

        public UserService(IUserRepository userRepository, IUnityOfWork unityOfWork)
        {
            _userRepository = userRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<bool> AuthLoginAsync(User user)
        {
            return await _userRepository.AuthLoginAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetAsync();
        }

        public async Task<UserResponse> PostAsync(User user)
        {
            try
            {
                await _userRepository.PostAsync(user);
                await _unityOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred when saving user: {ex.Message}");
            }
        }
    }
}
