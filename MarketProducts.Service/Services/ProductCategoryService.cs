using AutoMapper;
using MarketProducts.Data.IRepositories;
using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.Exceptions;
using MarketProducts.Service.Helpers;
using MarketProducts.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IMapper mapper;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(PaginationParams @params, 
                                                                              Expression<Func<ProductCategory,
                                                                              bool>> expression = null)
        {
            var pagedList = productCategoryRepository.GetAll(expression, isTracing: false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoryWithProductsAsync(PaginationParams @params, 
                                                                                        Expression<Func<ProductCategory,
                                                                                        bool>> expression = null)
        {
            var pagedList = productCategoryRepository.GetAll(expression, "Products", false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryAsync(Expression<Func<ProductCategory, bool>> expression)
        {
            var category = await productCategoryRepository.GetAsync(expression);
            if (category is null)
                throw new MarketException(404, "Product category not found");

            return category;
        }

        public async Task<ProductCategory> AddCategoryAsync(string category)
        {
            if(category == null) throw new ArgumentNullException(nameof(category));

            var newCategory = new ProductCategory
            {
                Name = category
            };
            var result = await productCategoryRepository.AddAsync(newCategory);

            await productCategoryRepository.SaveChangesAsync();
            return result;
        }

        public async Task<ProductCategory> UpdateCategoryAsync(long id, string dto)
        {
            var existingCategory = await productCategoryRepository.GetAsync(p => p.Id == id);
            if (existingCategory is null)
                throw new MarketException(404, "Product category not found");

            existingCategory.Name = dto;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            productCategoryRepository.Update(existingCategory);
            await productCategoryRepository.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Expression<Func<ProductCategory, bool>> expression)
        {
            var existCategory = await productCategoryRepository.GetAsync(expression);
            if (existCategory is null)
                throw new MarketException(404, "Product category not found");

            await productCategoryRepository.DeleteAsync(expression);
            await productCategoryRepository.SaveChangesAsync();

            return true;
        }
    }
}
