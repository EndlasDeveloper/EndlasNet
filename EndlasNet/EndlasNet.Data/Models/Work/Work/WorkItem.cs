using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkItem
    {
        [Key]
        public Guid WorkItemId { get; set; }

        [ForeignKey("WorkId")]
        [Display(Name ="Work Endlas number")]
        public Guid? WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("StaticPartInfoId")]
        [Display(Name ="Part info")]
        public Guid? StaticPartInfoId { get; set; }
        public virtual StaticPartInfo StaticPartInfo { get; set; }

        public Status Status { get; set; } = Status.NotStarted;

        [Display(Name = "Date started")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Date completed")]
        public DateTime? CompleteDate { get; set; }

        public bool IsInitialized { get; set; } = false;

        [NotMapped]
        [Display(Name = "Work item image")]
        public IFormFile WorkItemImageFile { get; set; }
        [NotMapped]
        public string WorkItemImageUrl { get; set; }
        [NotMapped]
        public bool ClearWorkItemImg { get; set; } = false;
        public byte[] WorkItemImageBytes { get; set; }

        public IEnumerable<PartForWork> PartsForWork { get; set; }
        public IEnumerable<MachiningToolForWork> MachiningToolsForWork { get; set; }
    }
}
