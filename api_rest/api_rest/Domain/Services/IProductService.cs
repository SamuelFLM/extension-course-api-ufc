using api_rest.Communication;
using api_rest.Domain.Models;
using System.Runtime.InteropServices.Marshalling;

namespace api_rest.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<ProductResponse> GetByIdAsync(int id);
        Task<ProductResponse> PostAsync(Product product);
        Task<ProductResponse> PutAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
