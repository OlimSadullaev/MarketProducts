using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.Interfaces;
using MarketProducts.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductCategoryService productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        /// <summary>
        /// CRUD of Category of product
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        [HttpPost("categories")]
        public async Task<ActionResult<ProductCategory>> AddCategoryAsync([FromBody] string name)
        {
            try
            {
                return Ok(await productCategoryService.AddCategoryAsync(name));
            }
            catch(ArgumentNullException) 
            {
                return BadRequest("Category name cannot be null.");
            }
        } 

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategoryAsync([FromQuery] PaginationParams @params)
        {
            try
            {
                var categoriesWithProducts = await productCategoryService.GetAllCategoryWithProductsAsync(@params);
                return Ok(categoriesWithProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching categories.");
            }
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] long id)
        {
            var category = await productCategoryService.GetCategoryAsync(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] long id, string name)
        {
            var updatedCategory = await productCategoryService.UpdateCategoryAsync(id, name);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] long id)
        {
            var isDeleted = await productCategoryService.DeleteCategoryAsync(p => p.Id == id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
