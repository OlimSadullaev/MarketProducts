using MarketProducts.Data.DbContexts;
using MarketProducts.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Data.Repositories
{
    public abstract class Repository<TSource> : IRepository<TSource> where TSource : class
    {
        private MarketDbContext dbContext;

        protected Repository(MarketDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<TSource> AddAsync(TSource entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleAsync(Expression<Func<TSource, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracing = true)
        {
            throw new NotImplementedException();
        }

        public Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null)
        {
            throw new NotImplementedException();
        }

        public Task SavechangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TSource> UpdateAsync(TSource entity)
        {
            throw new NotImplementedException();
        }
    }
}
