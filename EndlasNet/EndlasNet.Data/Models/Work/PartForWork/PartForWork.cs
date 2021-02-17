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

        [ForeignKey("PartId")]
        [Display(Name = "Part")]
        public Guid? PartId { get; set; }
        public virtual Part Part { get; set; }

        [ForeignKey("WorkId")]
        [Display(Name = "Job")]
        public Guid? WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
