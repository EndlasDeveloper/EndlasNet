using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class PartForJob
    {
        public Guid PartForJobId { get; set; }

        [ForeignKey("PartId")]
        [Display(Name = "Part")]
        public Guid? PartId { get; set; }
        public virtual Part Part { get; set; }

        [ForeignKey("JobId")]
        [Display(Name = "Job")]
        public Guid? JobId { get; set; }
        public virtual Job Job { get; set; }
    }
}
