using AutoMapper;
using MarketProducts.Data.IRepositories;
using MarketProducts.Data.Repositories;
using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.DTOs;
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
    public partial class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            this.productRepository = repository;
            _mapper = mapper;
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<Product> AddAsync(ProductForCreationDTO dto)
        {
            var mappedProduct = _mapper.Map<Product>(dto);
            var product = await productRepository.AddAsync(mappedProduct);

            await productRepository.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await productRepository.GetAsync(expression);
            if (product is null)
                throw new MarketException(404, "Product not found");

            await productRepository.DeleteAsync(expression);
            await productRepository.SaveChangesAsync();

            return true;
        }

        // in this method at the line of 56 I have changed isTracking to isTracing starts with var pagedList
        public async Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
        {
            var pagedList = productRepository.GetAll(expression, isTracing: false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
        {
            var pagedList = productRepository.GetAll(expression, "Category", false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null)
        {
            return await productRepository.GetAsync(expression);
        }

        public async Task<Product> UpdateAsync(long id, ProductForCreationDTO dto)
        {
            var product = await productRepository.GetAsync(p => p.Id == id);
            if (product is null)
                throw new MarketException(404, "Product not found");

            var mappedProduct = _mapper.Map(dto, product);
            var updatedProduct = productRepository.Update(mappedProduct);
            await productRepository.SaveChangesAsync();

            return updatedProduct;
        }
    }
}
