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
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Product CRUD
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await productService.GetAllWithCategoriesAsync(@params));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] long id)
        => Ok(await productService.GetAsync(p => p.Id == id));

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductForCreationDTO dto)
            => Ok(await productService.AddAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, ProductForCreationDTO dto)
            => Ok(await productService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
            => Ok(await productService.DeleteAsync(p => p.Id == id));

        /*[HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var productsWithCategories = await productService.GetAllWithCategoriesAsync(paginationParams);
                return Ok(productsWithCategories);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                // In a real-world application, you might handle specific exception types differently.
                // For example, you might return a custom error response for specific exceptions.
                // For simplicity, we'll return a generic error message here.
                return StatusCode(500, "An error occurred while fetching products.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            try
            {
                var product = await productService.GetAsync(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Return 404 Not Found when the product with the specified ID is not found.
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                return StatusCode(500, "An error occurred while fetching the product.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductForCreationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request with validation errors if the DTO is invalid.
            }

            try
            {
                var newProduct = await productService.AddAsync(dto);
                return CreatedAtAction(nameof(GetAsync), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, ProductForCreationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request with validation errors if the DTO is invalid.
            }

            try
            {
                var updatedProduct = await productService.UpdateAsync(id, dto);
                if (updatedProduct == null)
                {
                    return NotFound(); // Return 404 Not Found when the product with the specified ID is not found.
                }
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            try
            {
                var isDeleted = await productService.DeleteAsync(p => p.Id == id);
                if (!isDeleted)
                {
                    return NotFound(); // Return 404 Not Found when the product with the specified ID is not found.
                }
                return NoContent(); // Return 204 No Content for successful deletion with no response body.
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }*/



        ///// <summary>
        ///// CRUD of Category of product
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>

        //[HttpPost("categories")]
        //public async Task<ActionResult<ProductCategory>> AddCategoryAsync([FromBody] string name)
        //    => Ok(await _productService.AddCategoryAsync(name));
        //
        //[HttpGet("categories")]
        //public async Task<IActionResult> GetAllCategoryAsync([FromQuery] PaginationParams @params)
        //=> Ok(await _productService.GetAllCategoryWithProductsAsync(@params));
        //
        //[HttpGet("categories/{id}")]
        //public async Task<IActionResult> GetCategoryAsync([FromRoute] long id)
        //=> Ok(await _productService.GetCategoryAsync(p => p.Id == id));
        //
        //[HttpPut("categories/{id}")]
        //public async Task<IActionResult> UpdateCategoryAsync([FromRoute] long id, string name)
        //    => Ok(await _productService.UpdateCategoryAsync(id, name));
        //
        //[HttpDelete("categories/{id}")]
        //public async Task<IActionResult> DeleteCategoryAsync([FromRoute] long id)
        //    => Ok(await _productService.DeleteCategoryAsync(p => p.Id == id));
    }
}
 