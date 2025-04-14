using api_rest.Domain.Repositories;
using api_rest.Persistence.Contexts;

namespace api_rest.Persistence.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnityOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
