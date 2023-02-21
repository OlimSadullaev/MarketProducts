using MarketProducts.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Domain.Entities.Products
{
    public class ProductCategory : Auditable<long>
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
