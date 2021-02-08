using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class MachiningToolForJob
    {
        public Guid MachiningToolForJobId { get; set; }
        [Display(Name = "Date used")]
        public DateTime DateUsed { get; set; }

        [ForeignKey("JobId")]
        [Display(Name = "Job")]
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
