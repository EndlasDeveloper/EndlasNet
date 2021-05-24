using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;

namespace EndlasNet.Web.Models
{
    public class PartForWorkAndImgViewModel
    {
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; }

        public Guid StaticPartInfoId { get; set; }
        public int NumParts { get; set; }
        public Guid WorkId { get; set; }

    }
}
