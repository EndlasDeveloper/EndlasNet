using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web
{
    public class FileURL
    {
        public static void SetImageURL(StaticPartInfo staticPartInfo)
        {
            if (staticPartInfo.DrawingImageBytes != null)
            {
                string imageUrl = GetImageURL(staticPartInfo.DrawingImageBytes);
                staticPartInfo.ImageUrl = imageUrl;
            }
        }
        public static void SetImageURL(PartForWorkImg partForWorkImg)
        {
            string imageUrl = GetImageURL(partForWorkImg.ImageBytes);
            partForWorkImg.ImageUrl = imageUrl;
        }

        public static string GetImageURL(byte[] imgBytes)
        {
            string imgBase64Data = Convert.ToBase64String(imgBytes);
            return string.Format("data:image/png;base64,{0}", imgBase64Data);

        }

        public static string GetPdfUrl(byte[] pdfBytes)
        {

            string imageBase64Data = Convert.ToBase64String(pdfBytes);
            return string.Format("data:image/png;base64,{0}", imageBase64Data);
        }

        public static async Task<byte[]> GetFileBytes(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        internal static void SetImageURL(PartForJob partForJob)
        {
            throw new NotImplementedException();
        }
    }
}
