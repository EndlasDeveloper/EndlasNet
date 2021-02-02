using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Job
    {
        // PK
        public Guid JobId { get; set; }
        public string EndlasNumber { get; set; }
        public string JobDescription { get; set; }
        public string PurchaseOrderNum { get; set; }
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        // Job has 0:Many InsertToJobs
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        // Job has 0:Many MillToolToJobs
        public IEnumerable<MillToolToJob> MillToolToJobs { get; set; }
        // Job has 0:Many DrillBitToJobs
        public IEnumerable<DrillBitToJob> DrillBitToJobs { get; set; }
    }
}
