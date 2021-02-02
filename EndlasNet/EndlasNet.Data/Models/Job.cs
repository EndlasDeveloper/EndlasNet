using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Job
    {
        public Guid JobId { get; set; }
        public string EndlasNumber { get; set; }
        public string JobDescription { get; set; }
        public string Status { get; set; } = "Not Started";
        public string PurchaseOrderNum { get; set; }
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }

    }
}
