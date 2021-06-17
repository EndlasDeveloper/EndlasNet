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
        public Guid WorkId { get; set; }
        public Work Work { get; set; }

        [ForeignKey("StaticPartInfoId")]
        [Display(Name ="Part info")]
        public Guid? StaticPartInfoId { get; set; }
        public virtual StaticPartInfo StaticPartInfo { get; set; }

        public Status Status { get; set; } = Status.NotStarted;

        [Display(Name = "Complete date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Complete date")]
        public DateTime? CompleteDate { get; set; }


        public bool IsInitialized { get; set; } = false;

        public IEnumerable<PartForWork> PartsForWork { get; set; }

        [NotMapped]
        [Range(1, 200)]
        [Display(Name ="Number of parts")]
        public int NumPartsForWork { get; set; }

    }
}
