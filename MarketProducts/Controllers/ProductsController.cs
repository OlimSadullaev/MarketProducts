using MarketProducts.Data.IRepositories;
using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.DTOs;
using MarketProducts.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MarketProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Product CRUD
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _productService.GetAllWithCategoriesAsync(@params));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
        => Ok(await _productService.GetAsync(p => p.Id == id));

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductForCreationDTO dto)
            => Ok(await _productService.AddAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, ProductForCreationDTO dto)
            => Ok(await _productService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Ok(await _productService.DeleteAsync(p => p.Id == id));
    
        /// <summary>
        /// CRUD of Category of product
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("categories")]
        public async Task<ActionResult<ProductCategory>> AddCategoryAsync([FromBody] string name)
            => Ok(await _productService.AddCategoryAsync(name));

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategoryAsync([FromQuery] PaginationParams @params)
        => Ok(await _productService.GetAllCategoryWithProductsAsync(@params));

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] long id)
        => Ok(await _productService.GetCategoryAsync(p => p.Id == id));

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] long id, string name)
            => Ok(await _productService.UpdateCategoryAsync(id, name));

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] long id)
            => Ok(await _productService.DeleteCategoryAsync(p => p.Id == id));

    }
}
