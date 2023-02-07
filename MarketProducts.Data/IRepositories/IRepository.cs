using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Data.IRepositories
{
    public interface IRepository<TSource> where TSource : class
    {
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracing = true);
        Task<TSource> AddAsync(TSource entity);
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null);
        Task<TSource> UpdateAsync(TSource entity);
        Task DeleAsync(Expression<Func<TSource, bool>> expression);
        Task SavechangesAsync();
    }
}
