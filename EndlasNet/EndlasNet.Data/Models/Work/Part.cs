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
    public class Part 
    {
        public Guid PartId { get; set; }
        [Display(Name = "Drawing number")]
        public string DrawingNumber { get; set; }
        [Display(Name = "Condition description")]
        public string ConditionDescription { get; set; }
        [Display(Name = "Initial weight (lbs)")]
        public float InitWeight { get; set; }
        [Display(Name = "Weight (lbs)")]
        public float Weight{ get; set; }
        [Display(Name = "Cladded weight (lbs)")]
        public float CladdedWeight { get; set; }
        [Display(Name = "Processing notes")]
        public string ProcessingNotes { get; set; }

        [Display(Name = "Drawing image")]
        public byte[] DrawingImage { get; set; }

        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<PartForJob> PartsForJobs { get; set; }
    }
}
