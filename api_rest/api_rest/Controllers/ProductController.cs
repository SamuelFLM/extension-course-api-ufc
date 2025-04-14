using api_rest.Communication;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();

                var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                var result = _mapper.Map<Product, ProductResource>(product.Product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource saveProductResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessage());

                var convertToProduct = _mapper.Map<SaveProductResource, Product>(saveProductResource);

                var productReponse = await _productService.PostAsync(convertToProduct);

                if (!productReponse.Success)
                    return BadRequest(productReponse.Message);

                var product = _mapper.Map<Product, ProductResource>(productReponse.Product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource saveProductResource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessage());

                var product = _mapper.Map<SaveProductResource, Product>(saveProductResource);

                var update = await _productService.PutAsync(id, product);

                var result = _mapper.Map<Product, ProductResource>(update.Product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var productResponse = await _productService.DeleteAsync(id);

                if (!productResponse.Success)
                    return BadRequest(productResponse.Message);

                var result = _mapper.Map<Product, ProductResource>(productResponse.Product);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
