﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class PartForAWorkViewModel
    {

        public PartForWork PartForWork;
        public Guid PartForWorkId { get; set; }

        public PartForWorkImg PartForWorkImg { get; set; }
        public Guid PartForWorkImgId { get; set; }

        [Display(Name = "Part image")]
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Image name")]
        public string ImageName { get; set; }
        public bool ClearImg { get; set; } = false;



        [Display(Name = "Machining image")]
        public IFormFile MachiningImageFile { get; set; }
        public string MachiningImageUrl { get; set; }
        public bool ClearMachiningImg { get; set; } = false;



        [Display(Name = "Cladding image")]
        public IFormFile CladdingImageFile { get; set; }
        public string CladdingImageUrl { get; set; }
        public bool ClearCladdingImg { get; set; } = false;



        [Display(Name = "Finished image")]
        public IFormFile FinishedImageFile { get; set; }
        public string FinishedImageUrl { get; set; }
        public bool ClearFinishedImg { get; set; } = false;



        [Display(Name = "Used image")]
        public IFormFile UsedImageFile { get; set; }
        public string UsedImageUrl { get; set; }
        public bool ClearUsedImg { get; set; } = false;

        public void InitImageUrls()
        {
            MachiningImageUrl = FileURL.GetImageURL(PartForWork.MachiningImageBytes);
            CladdingImageUrl = FileURL.GetImageURL(PartForWork.CladdingImageBytes);
            FinishedImageUrl = FileURL.GetImageURL(PartForWork.FinishedImageBytes);
            UsedImageUrl = FileURL.GetImageURL(PartForWork.UsedImageBytes);
        }
    }
}
