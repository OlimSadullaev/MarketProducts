using MarketProducts.Data.Repositories;
using MarketProducts.Domain.Entities.Attachments;
using MarketProducts.Service.DTOs;
using MarketProducts.Service.Exceptions;
using MarketProducts.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly AttachmentRepository attachmentRepository;

        public AttachmentService(AttachmentRepository attachmentRepository)
        {
            this.attachmentRepository = attachmentRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var photo = await _attachmentService.FindAsync(id);
            throw new NotImplementedException();
        }

        public async Task<Attachment> UpdateAsync(AttachmentForCreationDTO dto)
        {
            var existAttachment = await attachmentRepository.GetAsync();

            if (existAttachment != null)
                throw new MarketException(404, "Attachment not found");

            string fileName = existAttachment.Path;
            string filePath = Path.Combine(dto.Path, fileName);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            //await dto.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            await attachmentRepository.SaveChangesAsync();

            return existAttachment;
        }

        public Task<Attachment> UploadAsync(AttachmentForCreationDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
