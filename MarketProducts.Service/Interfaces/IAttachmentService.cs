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
        Task<Attachment> UploadAsync(AttachmentForCreationDTO dto);
        Task<bool> DeleteAsync(long id);
        Task<Attachment> UpdateAsync(long id, Stream file);
    }
}
