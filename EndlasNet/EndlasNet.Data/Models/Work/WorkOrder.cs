using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkOrder : IWork
    {
        public Guid WorkId { get; set; }
        [Display(Name = "Endlas number")]
        public string EndlasNumber { get; set; }
        [Display(Name = "Work description")]
        public string WorkDescription { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; } = Status.Not_Started;

        // will be null
        public string PurchaseOrderNum { get; set; } = null;
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Customer Customer { get; set; } = null;
        public IEnumerable<PartForWorkOrder> PartsForWorkOrders { get; set; }
        public IEnumerable<MachiningToolForWorkOrder> ToolsForWorksOrders { get; set; }
    }
}
