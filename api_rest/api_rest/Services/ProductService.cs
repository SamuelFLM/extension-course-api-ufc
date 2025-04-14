using api_rest.Communication;
using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Domain.Services;

namespace api_rest.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnityOfWork _unityOfWork;

        public ProductService(IProductRepository repository, IUnityOfWork unityOfWork)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return new ProductResponse("Product not found.");
            try
            {

                _repository.Delete(product);
                await _unityOfWork.CompleteAsync();

                return new ProductResponse(product);

            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error ocurred when deleting product: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductResponse> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product is null)
                return new ProductResponse("Product not found.");

            return new ProductResponse(product);
        }

        public async Task<ProductResponse> PostAsync(Product product)
        {
            try
            {
                await _repository.PostAsync(product);
                await _unityOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch(Exception ex)
            {
                return new ProductResponse($"An error ocurred when saving product: {ex.Message}");
            }
        }

        public async Task<ProductResponse> PutAsync(int id, Product product)
        {
            var existingProduct = await _repository.GetByIdAsync(id);

            if (existingProduct is null)
                return new ProductResponse("Product not found.");

            try
            {
                existingProduct.Update(product.Name, product.QuantityInPackage, product.UnitOfMeasurement);

                _repository.Update(existingProduct);
                await _unityOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error ocurred when updating product: {ex.Message}");
            }

        }
    }
}
