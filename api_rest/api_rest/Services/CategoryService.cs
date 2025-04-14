using api_rest.Communication;
using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Domain.Services;

namespace api_rest.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnityOfWork _unityOfWork;
        public CategoryService(ICategoryRepository categoryRepository, IUnityOfWork unityOfWork)
        {
            _categoryRepository = categoryRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRepository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {

                await _categoryRepository.AddAsync(category);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error ocurred when saving the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {

            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory is null)
                return new CategoryResponse("Category not Found.");

            existingCategory.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error ocurred when updating the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {

            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not Found.");

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unityOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"An error ocurred when deleting the category: {ex.Message}");
            }

        }
    }
}
