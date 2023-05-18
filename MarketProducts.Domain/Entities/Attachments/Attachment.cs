using MarketProducts.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Domain.Entities.Attachments
{
    public class Attachment : Auditable<long>
    {
        public string Path { get; set; }
    }
}
