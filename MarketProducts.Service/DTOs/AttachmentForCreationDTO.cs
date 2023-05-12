using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProducts.Service.DTOs
{
    public class AttachmentForCreationDTO
    {
        public string Path { get; set; }
        public Stream Stream { get; set; }
    }
}
