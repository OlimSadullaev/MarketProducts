using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using MarketProducts.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Interfaces
{
    public partial interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
        Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
        Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null);
        Task<Product> AddAsync(ProductForCreationDTO dto);
        Task<Product> UpdateAsync(long id, ProductForCreationDTO dto);
        Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
    }
}
