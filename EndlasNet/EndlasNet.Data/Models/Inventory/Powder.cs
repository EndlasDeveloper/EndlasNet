using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Powder
    {
        [Key]
        public Guid PowderId { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Powder name")]
        public string PowderName { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Vendor description")]
        public string VendorDescription { get; set; }

        [Required]
        [Display(Name = "PO number")]
        public string PoNumber { get; set; }

        [Required]
        [Display(Name = "PO date")]
        public DateTime PoDate { get; set; }

        [Required]
        [Display(Name = "Bottle number")]
        public string BottleNumber { get; set; }

        [Required]
        [Range(0f,2000f)]
        [Display(Name = "Particle size (um)")]
        public float ParticleSize { get; set; }

        [Required]
        [Range(1.0f, 1000.0f)]
        [Display(Name = "Initial weight (lbs)")]
        public float InitWeight { get; set; }

        [Required]
        [Range(0f,1000.0f)]
        [Display(Name = "Weight (lbs)")]
        public float Weight { get; set; }

        [Required]
        [Range(0f,float.MaxValue)]
        [Display(Name = "Cost per pound")]
        public float CostPerUnitWeight { get; set; }
        [Display(Name = "Lot number")]
        public string LotNumber { get; set; }
        // FK references
        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        [Display(Name = "Vendor")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

    }
}
