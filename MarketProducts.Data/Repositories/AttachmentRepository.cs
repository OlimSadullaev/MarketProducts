using MarketProducts.Data.DbContexts;
using MarketProducts.Data.IRepositories;
using MarketProducts.Domain.Entities.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Data.Repositories
{
    public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(MarketDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
