using MarketProducts.Service.Extensions;
using MarketProducts.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketProducts.Controllers
{
    [ApiController]
    [Route("api/attachments")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            return Ok(await attachmentService.UploadAsync(formFile.ToAttachmentOrDefault()));
        }
    }
}
