using MarketProducts.Domain.Entities.Attachments;
using MarketProducts.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Interfaces
{
    public interface IAttachmentService
    {
        public Task<Attachment> UploadAsync(AttachmentForCreationDTO dto);
        public Task<bool> DeleteAsync(int id);
        public Task<Attachment> UpdateAsync(AttachmentForCreationDTO dto);
    }
}
