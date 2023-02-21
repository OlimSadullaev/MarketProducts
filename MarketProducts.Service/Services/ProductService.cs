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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = repository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<Product> AddAsync(ProductForCreationDTO dto)
        {
            var mappedProduct = _mapper.Map<Product>(dto);
            var product = await _productRepository.AddAsync(mappedProduct);

            await _productRepository.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _productRepository.GetAsync(expression);
            if (product is null)
                throw new MarketException(404, "Product not found");

            await _productRepository.DeleteAsync(expression);
            await _productRepository.SaveChangesAsync();

            return true;
        }


        // in this method at the line of 57 I have changed isTracking to isTracing
        public async Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
        {
            var pagedList = _productRepository.GetAll(expression, isTracing: false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
        {
            var pagedList = _productRepository.GetAll(expression, "Category", false).ToPagedList(@params);

            return await pagedList.ToListAsync();
        }

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null)
        {
            return await _productRepository.GetAsync(expression);
        }

        public async Task<Product> UpdateAsync(long id, ProductForCreationDTO dto)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);
            if (product is null)
                throw new MarketException(404, "Product not found");

            var mappedProduct = _mapper.Map(dto, product);
            var updatedProduct = await _productRepository.UpdateAsync(mappedProduct);
            await _productRepository.SaveChangesAsync();

            return updatedProduct;
        }
    }
}
