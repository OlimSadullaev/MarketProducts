using MarketProducts.Data.DbContexts;
using MarketProducts.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
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
        protected readonly MarketDbContext _dbcontext;
        protected readonly DbSet<TSource> _dbSet;

        public Repository(MarketDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _dbSet = dbcontext.Set<TSource>();
        }

        public async Task<TSource> AddAsync(TSource entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
        {
            var entity = await GetAsync(expression);
            _dbSet.Remove(entity);
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracing = true)
        {
            IQueryable<TSource> query = expression is null ? _dbSet : _dbSet.Where(expression);

            if(!string.IsNullOrEmpty(include))
                query = query.Include(include);

            if(!isTracing)
                query = query.Include(include);

            return query;
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null)
        {
            return await GetAll(expression, include).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public TSource Update(TSource entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
