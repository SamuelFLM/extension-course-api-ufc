using api_rest.Domain.Models;
using api_rest.Domain.Services;
using api_rest.Extensions;
using api_rest.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();

            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> PostAynsc([FromBody] SaveCategoryResource saveCategoryResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessage());

                var category = _mapper.Map<SaveCategoryResource, Category>(saveCategoryResource);
                var result = await _categoryService.SaveAsync(category);

                if (!result.Success)
                    return BadRequest(result.Message);

                var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);

                return Ok(categoryResource);
            }   
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource saveCategoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            var category = _mapper.Map<SaveCategoryResource, Category>(saveCategoryResource);
            var result = await _categoryService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);

            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);

            return Ok(categoryResource);
        }
    }
}
