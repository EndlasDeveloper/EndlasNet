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
        [Display(Name ="Powder name")]
        public string PowderName { get; set; }
        [Display(Name ="Vendor description")]
        public string VendorDescription { get; set; }
        [Display(Name ="Particle size (microns)")]
        public float ParticleSize { get; set; }

        [ForeignKey("PowderOrderId")]
        public Guid PowderOrderId { get; set; }
        public virtual PowderOrder PowderOrder { get; set; }

        [Display(Name ="Number of bottles")]
        public int NumBottles { get; set; }

        public IEnumerable<Powder> Powders { get; set; }

    }
}
