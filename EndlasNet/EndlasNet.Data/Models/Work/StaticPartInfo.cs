using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class StaticPartInfo
    {
        [Key]
        public Guid StaticPartInfoId { get; set; }

        [Required]
        [Display(Name = "Drawing number")]
        public string DrawingNumber { get; set; }

        [Required]
        [Display(Name ="Approx weight (lbs)")]
        public float ApproxWeight { get; set; }

        [Required]
        [Display(Name ="Part descritpion")]
        public string PartDescription { get; set; }

        [Display(Name = "Image title")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Upload image file")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageURL { get; set; }
        public byte[] DrawingImage { get; set; }

        [NotMapped]
        [Display(Name ="Upload drawing file")]
        public IFormFile DrawingFile { get; set; }

        public byte[] DrawingPDF { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name ="Customer")]
        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public IEnumerable<PartForWork> Parts { get; set; }
    }
}
