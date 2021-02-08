using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndlasNet.Data
{
    public class PartForWorkOrder
    {
        public Guid PartForWorkOrderId { get; set; }

        [ForeignKey("PartId")]
        [Display(Name = "Part")]
        public Guid? PartId { get; set; }
        public virtual Part Part { get; set; }

        [ForeignKey("WorkOrderId")]
        [Display(Name = "WorkOrder")]
        public Guid? WorkOrderId { get; set; }
        public virtual WorkOrder Job { get; set; }
    }
}