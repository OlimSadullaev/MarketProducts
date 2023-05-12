using AutoMapper;
using MarketProducts.Data.Repositories;
using MarketProducts.Domain.Entities.Attachments;
using MarketProducts.Service.DTOs;
using MarketProducts.Service.Exceptions;
using MarketProducts.Service.Extensions;
using MarketProducts.Service.Helpers;
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
        private readonly IMapper mapper;

        public AttachmentService(AttachmentRepository attachmentRepository, IMapper mapper)
        {
            this.attachmentRepository = attachmentRepository;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var photo = await _attachmentService.FindAsync(id);
            throw new NotImplementedException();
        }

        public async Task<Attachment> UpdateAsync(int id, Stream file)
        {
            var existAttachment = await attachmentRepository.GetAsync(a => a.Id == id);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(existAttachment.Path);
            await fileStream.CopyToAsync(file);

            await fileStream.FlushAsync();
            fileStream.Close();

            return existAttachment;
        }

        public async Task<Attachment> UploadAsync(AttachmentForCreationDTO dto)
        {
            string fileName = Guid.NewGuid().ToString("N") + "png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            var attachment = await attachmentRepository.AddAsync(mapper.Map<Attachment>(dto));
            attachment.Path = filePath;
            await attachmentRepository.SaveChangesAsync();
            return attachment;
        }
    }
}
