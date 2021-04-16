using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Newtonsoft.Json;

namespace EndlasNet.Web.Models
{
    public class CheckBoxInfo
    {
        public bool IsChecked { get; set; } = false;
        [Display(Name ="Part")]
        public Guid PartForWorkId { get; set; }
        public string Label { get; set; }
        public string RuntimeId { get; set; }
    }

    public class PowderForPartViewModel
    {
        [Display(Name ="Work")]
        public Work Work { get; set; }
        [Display(Name = "Work")]
        public Guid WorkId { get; set; }
        [Display(Name = "Powder bottle")]
        public Guid PowderBottleId { get; set; }
        [Display(Name = "Powder weight used (lbs)")]
        [Range(0.0001, 200.0)]
        public float PowderWeightUsed { get; set; }
        public List<CheckBoxInfo> CheckBoxes { get; set; }
    }
    
}
