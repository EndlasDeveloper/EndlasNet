using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class PartForWorkImg
    {
        [Key]
        public Guid PartForWorkImgId { get; set; }

        [Display(Name = "Image name")]
        public string ImageName { get; set; }

        /******************** IMG ********************/
        [NotMapped]
        [Display(Name = "Part image")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }
        public byte[] ImageBytes { get; set; }

        /******************** END ********************/

        public IEnumerable<PartForWork> PartsForWork { get; set; }
    }
}
