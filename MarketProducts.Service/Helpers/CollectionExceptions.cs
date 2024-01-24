using MarketProducts.Domain.Configurations;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Helpers
{
    public static class CollectionExceptions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source , PaginationParams @params)
        {
            return @params.PageIndex > 0 && @params.PageSize >= 0
                ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageIndex)
                : source;
        }

        /*public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
        {
            var value = ((@params.PageIndex - 1) * @params.PageSize);

            return @params.PageIndex > 0 && @params.PageSize >= 0
                ? source.Take(value..(value + @params.PageSize))
                : source;
        }*/
    } 
}
