using MarketProducts.Data.IRepositories;
using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.Exceptions;
using MarketProducts.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Services
{
    public partial class ProductService
    {
        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null)
        {
            var pagedList = _productCategoryRepository.GetAll(expression, isTracing: false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<ProductCategory, bool>> expression = null)
        {
            var pagedList = _productCategoryRepository.GetAll(expression, "Products", false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryAsync(Expression<Func<ProductCategory, bool>> expression)
        {
            var category = await _productCategoryRepository.GetAsync(expression);
            if (category is null)
                throw new MarketException(404, "Product category not found");

            return category;
        }

        public async Task<ProductCategory> AddCategoryAsync(string category)
        {
            var newCategory = new ProductCategory();
            {
                newCategory.Name = category;
            }
            var result = await _productCategoryRepository.AddAsync(newCategory);

            await _productCategoryRepository.SaveChangesAsync();
            return result;
        }

        public async Task<ProductCategory> UpdateCategoryAsync(long id, string dto)
        {
            var existingCategory = await _productCategoryRepository.GetAsync(p => p.Id == id);
            if (existingCategory is null)
                throw new MarketException(404, "Product category not found");

            existingCategory.Name = dto;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            _productCategoryRepository.Update(existingCategory);
            await _productCategoryRepository.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Expression<Func<ProductCategory, bool>> expression)
        {
            var existCategory = await _productCategoryRepository.GetAsync(expression);
            if (existCategory is null)
                throw new MarketException(404, "Product category not found");

            await _productCategoryRepository.DeleteAsync(expression);
            await _productCategoryRepository.SaveChangesAsync();

            return true;
        }
    }
}
