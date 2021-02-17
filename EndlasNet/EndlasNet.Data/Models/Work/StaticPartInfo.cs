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
        [Display(Name ="Approx weight")]
        public float ApproxWeight { get; set; }

        [Required]
        [Display(Name ="Part descritpion")]
        public string PartDescription { get; set; }

        [Display(Name = "Image title")]
        public string ImageName { get; set; }

        [NotMapped]
        [Display(Name = "Upload file")]
        public IFormFile ImageFile { get; set; }
        
        public byte[] DrawingImage { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name ="Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("UserId")] 
        public Guid? UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<PartForWork> Parts { get; set; }
    }
}
