﻿using AutoMapper;
using MarketProducts.Data.IRepositories;
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
        private readonly IAttachmentRepository attachmentRepository;
        private readonly IMapper mapper;

        public AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper)
        {
            this.attachmentRepository = attachmentRepository;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var attachment = await attachmentRepository.GetAsync(a => a.Id == id);
            
            if (attachment == null)
                throw new MarketException(404, "attachment not found");
            
            await attachmentRepository.DeleteAsync(a => a.Id == id);
            File.Delete(attachment.Path);
            
            await attachmentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<Attachment> UpdateAsync(long id, Stream file)
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
            string fileName = Guid.NewGuid().ToString("N") + ".jpg";
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
