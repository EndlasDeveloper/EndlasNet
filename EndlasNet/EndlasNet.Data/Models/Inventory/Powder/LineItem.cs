using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class LineItem
    {
        [Key]
        public Guid LineItemId { get; set; }

        [ForeignKey("StaticPowderInfoId")]
        [Display(Name ="Powder name")]
        public Guid? StaticPowderInfoId { get; set; }
        public virtual StaticPowderInfo StaticPowderInfo { get; set; }
        [Display(Name ="Vendor description")]
        public string VendorDescription { get; set; }

        [Required]
        [Range(0f, float.MaxValue)]
        [Display(Name = "Weight (lbs)")]
        public float Weight { get; set; }

        [Required]
        [Range(0f, float.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Line item cost")]
        public float LineItemCost { get; set; }

        [Range(0f, float.MaxValue)]
        [Display(Name = "Minimum particle size (microns)")]
        public float ParticleSizeMin { get; set; }

        [Range(0f, float.MaxValue)]
        [Display(Name = "Maximum particle size (microns)")]
        public float ParticleSizeMax { get; set; }

        [ForeignKey("PowderOrderId")]
        public Guid PowderOrderId { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name ="Powder order number")]
        public virtual PowderOrder PowderOrder { get; set; }

        [Range(0, 1000)]
        [Display(Name ="Number of bottles")]
        public int NumBottles { get; set; }

        public IEnumerable<Powder> Powders { get; set; }


    }
}
