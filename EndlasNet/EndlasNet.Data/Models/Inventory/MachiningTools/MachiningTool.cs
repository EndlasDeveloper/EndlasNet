using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public enum ToolTypes
    {
        Insert,
        DrillBit,
        MillTool
    }
    public class MachiningTool
    {
        // PK
        public Guid MachiningToolId { get; set; }
        [Display(Name = "Tool type")]
        public ToolTypes ToolType { get; set; }
        [Display(Name = "Tool diameter")]
        public float ToolDiameter { get; set; }
        // columns
        [Display(Name = "Vendor description")]
        public string VendorDescription { get; set; }

        [Range(1, 1000)]
        [Display(Name = "Tool count")]
        public int ToolCount { get; set; }

        [StringLength(25)]
        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }
        [Display(Name = "Purchase order date")]
        public DateTime PurchaseOrderDate { get; set; }

        [Range(0f, 1000.0f)]
        [Display(Name = "Purchase order price")]
        public float PurchaseOrderPrice { get; set; }

        // FK references
        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        [Display(Name = "Vendor")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        [Display(Name = "Tool to job")]
        public virtual ToolToJob ToolToJob { get; set; }

    }
}
