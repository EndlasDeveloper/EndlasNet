using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Endlas number")]
        public string EndlasNumber { get; set; }
        [Display(Name = "Job description")]
        public string JobDescription { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; } = Status.Not_Started;
        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name = "Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public IEnumerable<PartForJob> PartsForJobs { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }

    }
}
