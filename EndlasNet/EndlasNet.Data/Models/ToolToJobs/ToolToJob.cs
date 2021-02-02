using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class ToolToJob
    {
        public Guid ToolToJobId { get; set; }
        public DateTime DateUsed { get; set; }

        [ForeignKey("JobId")]
        public Guid JobId { get; set; }
        public virtual Job Job { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
