using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Job : IWork
    {
        public Guid WorkId { get; set; }

        [Display(Name = "Endlas number")]
        public string EndlasNumber { get; set; }
        [Display(Name = "Job description")]
        public string WorkDescription { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; } = Status.NotStarted;
        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("CustomerId")]
        [Display(Name = "Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public IEnumerable<PartForJob> PartsForJobs { get; set; }
        public IEnumerable<MachiningToolForJob> ToolsForJobs { get; set; }
    }
}
