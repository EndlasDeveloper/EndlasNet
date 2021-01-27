using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class MachiningTool
    {
        // PK
        public Guid MachiningToolId { get; set; }
        // columns
        [StringLength(250)]
        public string VendorDescription { get; set; }

        [Range(1, 1000)]
        public int ToolCount { get; set; }

        [StringLength(25)]
        public string PurchaseOrderNum { get; set; }
        public DateTime PurchaseOrderDate { get; set; }

        [Range(0f, 1000.0f)]
        public float PurchaseOrderPrice { get; set; }

        // FK references
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ToolToJob ToolToJobs { get; set; }

    }
}
