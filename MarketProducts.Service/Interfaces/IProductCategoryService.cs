using MarketProducts.Domain.Configurations;
using MarketProducts.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Interfaces;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync(PaginationParams @params,
                                                             Expression<Func<ProductCategory, 
                                                             bool>> expression = null);
    Task<IEnumerable<ProductCategory>> GetAllCategoryWithProductsAsync(PaginationParams @params, 
                                                                       Expression<Func<ProductCategory,
                                                                       bool>> expression = null);
    Task<ProductCategory> GetCategoryAsync(Expression<Func<ProductCategory, bool>> expression = null);
    Task<ProductCategory> AddCategoryAsync(string category);
    Task<ProductCategory> UpdateCategoryAsync(long id, string dto);
    Task<bool> DeleteCategoryAsync(Expression<Func<ProductCategory, bool>> expression);
}

