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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsunc(long id)
            => Ok(await attachmentService.DeleteAsync(id));

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            return Ok(await attachmentService.UploadAsync(formFile.ToAttachmentOrDefault()));
        }
    }
}
