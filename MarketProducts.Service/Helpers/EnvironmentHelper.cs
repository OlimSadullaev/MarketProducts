
namespace MarketProducts.Service.Helpers
{
    public class EnvironmentHelper
    {
        public static string WebRootPath { get; set; }
        public static string AttachmentPath => Path.Combine(WebRootPath, "Images");
    }
}
