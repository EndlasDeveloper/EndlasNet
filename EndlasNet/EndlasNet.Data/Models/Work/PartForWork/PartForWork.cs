using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace EndlasNet.Data
{
    public class PartForWork 
    {
        [Key]
        public Guid PartForWorkId { get; set; }

        [ForeignKey("WorkId")]
        [Display(Name ="Work")]
        public Guid WorkId { get; set; }
        public Work Work { get; set; }

        [ForeignKey("StaticPartInfoId")]
        [Display(Name ="Part info")]
        public Guid StaticPartInfoId { get; set; }
        public StaticPartInfo StaticPartInfo { get; set; }

        public string Suffix { get; set; }

        [Range(1,10000)]
        [Display(Name = "Number of parts")]
        public int NumParts { get; set; }

        [Display(Name = "Condition description")]
        public string ConditionDescription { get; set; }

        [Display(Name = "Initial weight (lbs)")]
        public float? InitWeight { get; set; }

        [Display(Name = "Cladded weight (lbs)")]
        public float? CladdedWeight { get; set; } 

        [Display(Name = "Finished weight (lbs)")]
        public float? FinishedWeight { get; set; }

        [Display(Name = "Processing notes")]
        public string ProcessingNotes { get; set; }

        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        [Display(Name ="Starting suffix")]
        [RegularExpression(@"^[A-Z]*$")]
        public string StartSuffix { get; set; } = "A";

        [NotMapped]
        public string WorkType { get; set; }
    }
}
