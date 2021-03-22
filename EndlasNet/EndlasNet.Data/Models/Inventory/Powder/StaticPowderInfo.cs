using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EndlasNet.Data
{
    public class StaticPowderInfo
    {
        [Key]
        public Guid StaticPowderInfoId { get; set; }

        [Required]
        [Display(Name ="Powder name")]
        public string PowderName { get; set; }


        [Display(Name = "Powder description")]
        public string Description { get; set; }


        [Display(Name = "Chemical composition")]
        public string Composition { get; set; }

        [Required]
        [Display(Name ="Density (g per cc)")]
        public float Density { get; set; }

        [Display(Name ="Flow rate slope")]
        public float? FlowRateSlope { get; set; }

        [Display(Name ="Flow rate y-intercept")]
        public float? FlowRateYIntercept { get; set; }

        public IEnumerable<Powder> Powders { get; set; }
        public IEnumerable<LineItem> LineItems { get; set; }
    }
}
