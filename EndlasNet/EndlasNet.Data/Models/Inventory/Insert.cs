using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Insert
    {
        // PK
        public Guid InsertId { get; set; }
        // columns
        [StringLength(250)]
        public string Description { get; set; }

        [Range(1, 1000)]
        public int InsertCount { get; set; }
        public string PurchaseOrderNum { get; set; }
        public DateTime PurchaseOrderDate { get; set; }

        [Range(0f, float.MaxValue)]
        public float PurchaseOrderPrice { get; set; }

        [Range(0f, 1000.0f)]
        public float ToolTipRadius { get; set; }
        public string VendorPartNum { get; set; }
        // FK references
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual InsertToJob InsertToJob { get; set; }
    }
}
