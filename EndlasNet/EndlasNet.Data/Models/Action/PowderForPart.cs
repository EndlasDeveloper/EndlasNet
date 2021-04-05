using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class PowderForPart
    {
        [Key]
        public Guid PowderForPartId { get; set; }
        [ForeignKey("PowderBottleId")]
        [Display(Name ="PowderBottle")]
        public Guid? PowderBottleId { get; set; }
        public virtual PowderBottle PowderBottle { get; set; }
        [ForeignKey("PartForWorkId")]
        [Display(Name = "Part")]
        public Guid? PartForWorkId { get; set; }
        [Display(Name = "Part")]
        public virtual PartForWork PartForWork { get; set; }

        [Display(Name ="PowderBottle weight used (lbs)")]
        [Range(0.0001,200.0)]
        public float? PowderWeightUsed { get; set; }
    }
}
