using MarketProducts.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Domain.Entities.Products
{
    public class Product : Auditable<long>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public long CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}
