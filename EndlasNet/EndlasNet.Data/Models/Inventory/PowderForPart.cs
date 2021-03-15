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
        [ForeignKey("PowderId")]
        [Display(Name ="Powder")]
        public Guid PowderId { get; set; }
        public Powder Powder { get; set; }
        [ForeignKey("PartForWorkId")]
        [Display(Name = "Part")]
        public Guid PartForWorkId { get; set; }
        [Display(Name = "Part")]
        public PartForWork PartForWork { get; set; }

        [Display(Name ="Powder weight used (lbs)")]
        public float PowderWeightUsed { get; set; }
    }
}
