using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public enum Status
    {
        Not_Started,
        In_Progress,
        Complete,
        Past_Due
    }
    public class Job
    {
        public Guid JobId { get; set; }
        public string EndlasNumber { get; set; }
        public string JobDescription { get; set; }
        public Status Status { get; set; } = Status.Not_Started;
        public string PurchaseOrderNum { get; set; }
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public IEnumerable<PartForJob> PartsForJobs { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }

    }
}
