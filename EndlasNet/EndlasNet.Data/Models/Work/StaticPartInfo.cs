﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class StaticPartInfo
    {
        [Key]
        public Guid StaticPartInfoId { get; set; }


        [Required]
        [Display(Name = "Drawing number")]
        public string DrawingNumber { get; set; }

        [Required]
        [Display(Name ="Approx weight (lbs)")]
        public float ApproxWeight { get; set; }

        [Required]
        [Display(Name ="Part descritpion")]
        public string PartDescription { get; set; }

        [Display(Name = "Image title")]
        public string ImageName { get; set; }

        /************************* IMG ***************************/
        [NotMapped]
        [Display(Name = "Upload drawing image file")]
        public IFormFile ImageFile { get; set; }

        [NotMapped] // for showing thumbnail images for multiple rows in a single view
        public string ImageUrl { get; set; }
        public byte[] DrawingImageBytes { get; set; }


        /*********************** BLANK ***************************/
        [NotMapped]
        [Display(Name = "Upload blank drawing pdf")]
        public IFormFile BlankDrawingFile { get; set; }
        public byte[] BlankDrawingPdfBytes { get; set; }

        /*********************** FINISH ***************************/
        [NotMapped]
        [Display(Name ="Upload finish drawing pdf")]
        public IFormFile FinishDrawingFile { get; set; }
        public byte[] FinishDrawingPdfBytes { get; set; }    

        // Static part info has (describes) many PartsForWork
        public IEnumerable<WorkItem> WorkItems { get; set; }

        [NotMapped]
        [Display(Name ="Clear current image")]
        public bool ClearImg { get; set; }
        [NotMapped]
        [Display(Name = "Clear current blank file")]
        public bool ClearBlank { get; set; }
        [NotMapped]
        [Display(Name = "Clear current finish file")]
        public bool ClearFinish { get; set; }
    }
}
