using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
namespace EndlasNet.Data
{
    public static class FileURL
    {
        public static void SetImageURL(StaticPartInfo staticPartInfo)
        {
            if (staticPartInfo.DrawingImage != null)
            {
                string imageBase64Data = Convert.ToBase64String(staticPartInfo.DrawingImage);
                string imageUrl = string.Format("data:image/png;base64,{0}", imageBase64Data);
                staticPartInfo.ImageURL = imageUrl;
            }
        }


        public static async Task<byte[]> GetImageBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
