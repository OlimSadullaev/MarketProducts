using MarketProducts.Domain.Entities.Attachments;
using MarketProducts.Service.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.Extensions
{
    public static class FormFileExtensions
    {
        public static AttachmentForCreationDTO ToAttachmentOrDefault(this IFormFile formFile)
        {
            if (formFile?.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AttachmentForCreationDTO()
                {
                    Stream = new MemoryStream(fileBytes)
                };
            }

            return null;
        }
    }
}
