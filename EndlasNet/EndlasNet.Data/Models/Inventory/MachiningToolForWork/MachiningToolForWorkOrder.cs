using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkOrder
    {
        public Guid MachiningToolForJobId { get; set; }
        [Display(Name = "Date used")]
        public DateTime DateUsed { get; set; }

        [ForeignKey("WorkOrderId")]
        [Display(Name = "Work order")]
        public Guid WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}