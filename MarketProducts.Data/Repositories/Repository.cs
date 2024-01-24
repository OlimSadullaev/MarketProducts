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
        protected readonly MarketDbContext dbcontext;
        protected readonly DbSet<TSource> dbSet;

        public Repository(MarketDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
            this.dbSet = dbcontext.Set<TSource>();
        }

        public async Task<TSource> AddAsync(TSource entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
        {
            var entity = await GetAsync(expression);
            dbSet.Remove(entity);
        }

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracing = true)
        {
            IQueryable<TSource> query = expression is null ? dbSet : dbSet.Where(expression);

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
            await dbcontext.SaveChangesAsync();
        }

        public TSource Update(TSource entity)
        {
            return dbSet.Update(entity).Entity;
        }
    }
}
