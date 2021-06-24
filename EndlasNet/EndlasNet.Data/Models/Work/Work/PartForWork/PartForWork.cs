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

        [ForeignKey("WorkItemId")]
        [Display(Name ="Work item")]
        public Guid? WorkItemId { get; set; }
        public virtual WorkItem WorkItem { get; set; }

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

        [NotMapped]
        [Display(Name ="Starting suffix")]
        [RegularExpression(@"^[A-Z]*$")]
        public string StartSuffix { get; set; } = "A";

        [NotMapped]
        public string WorkType { get; set; }
        [NotMapped]
        public string DrawingNumberSuffix { get; set; }

        [Display(Name ="Part image")]
        public Guid? PartForWorkImgId { get; set; }
        public PartForWorkImg PartForWorkImg{ get; set; }


        [NotMapped]
        [Display(Name = "Part image")]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [Display(Name = "Image name")]
        public string ImageName { get; set; }
        public bool ClearImg { get; set; } = false;


        [NotMapped]
        [Display(Name = "Machining image")]
        public IFormFile MachiningImageFile { get; set; }
        [NotMapped]
        public string MachiningImageUrl { get; set; }
        [NotMapped]
        public bool ClearMachiningImg { get; set; } = false;
        public byte[] MachiningImageBytes { get; set; }


        [NotMapped]
        [Display(Name = "Cladding image")]
        public IFormFile CladdingImageFile { get; set; }
        [NotMapped]
        public string CladdingImageUrl { get; set; }
        [NotMapped]
        public bool ClearCladdingImg { get; set; } = false;
        public byte[] CladdingImageBytes { get; set; }

        [NotMapped]
        [Display(Name = "Finished image")]
        public IFormFile FinishedImageFile { get; set; }
        [NotMapped]
        public string FinishedImageUrl { get; set; }
        [NotMapped]
        public bool ClearFinishedImg { get; set; } = false;
        public byte[] FinishedImageBytes { get; set; }


        [NotMapped]
        [Display(Name = "Used image")]
        public IFormFile UsedImageFile { get; set; }
        [NotMapped]
        public string UsedImageUrl { get; set; }
        [NotMapped]
        public bool ClearUsedImg { get; set; } = false;

        public byte[] UsedImageBytes { get; set; }

        public IEnumerable<PowderForPart> PowdersUsed { get; set; }

    }
}
