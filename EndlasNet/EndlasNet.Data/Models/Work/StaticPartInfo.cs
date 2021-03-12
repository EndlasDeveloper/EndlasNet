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

        /************************* IMG ***************************/
        [NotMapped]
        [Display(Name = "Upload drawing image file")]
        public IFormFile ImageFile { get; set; }

        [NotMapped] // for showing thumbnail images for multiple rows in a single view
        public string ImageUrl { get; set; }
        public byte[] DrawingImageBytes { get; set; }
        /********************** END IMG ***************************/


        /************************ PDF ****************************/
        [NotMapped]
        [Display(Name ="Upload finish drawing pdf")]
        public IFormFile DrawingFile { get; set; }

        public byte[] DrawingPdfBytes { get; set; }
        /********************** END PDF ***************************/


        [ForeignKey("CustomerId")]
        [Display(Name ="Customer")]
        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        // Static part info has (describes) many PartsForWork
        public IEnumerable<PartForWork> PartsForWork { get; set; }
    }
}
