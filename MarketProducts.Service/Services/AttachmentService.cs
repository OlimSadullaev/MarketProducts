using MarketProducts.Domain.Entities.Attachments;
using MarketProducts.Service.DTOs;
using MarketProducts.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Services
{
    public class AttachmentService : IAttachmentService
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> UpdateAsync(AttachmentForCreationDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> UploadAsync(AttachmentForCreationDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
