using api_rest.Domain.Models;

namespace api_rest.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task PostAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
