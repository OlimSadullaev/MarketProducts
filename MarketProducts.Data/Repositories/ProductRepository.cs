using MarketProducts.Data.DbContexts;
using MarketProducts.Data.IRepositories;
using MarketProducts.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MarketDbContext dbContext) : base(dbContext)
        {

        }
    }
}
