using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public static class ImageURL
    {
        public static void SetImageURL(StaticPartInfo staticPartInfo)
        {
            if (staticPartInfo.DrawingImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(staticPartInfo.DrawingImage);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                staticPartInfo.ImageURL = imageDataURL;
            }
        }
        public static async Task<byte[]> GetBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
