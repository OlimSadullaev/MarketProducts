using MarketProducts.Domain.Commons;
using MarketProducts.Domain.Entities.Attachments;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Domain.Entities.Products
{
    public class Product : Auditable<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public long CategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public long? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
