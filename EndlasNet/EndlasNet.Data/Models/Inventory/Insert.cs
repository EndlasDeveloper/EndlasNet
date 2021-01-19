using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Insert
    {
        // PK
        public int InsertId { get; set; }
        // columns
        public string Description { get; set; }
        public int InsertCount { get; set; }
        public DateTime DateAdded { get; set; }
        public string PurchaseOrderNum { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public float PurchaseOrderPrice { get; set; }
        public float ToolTipRadius { get; set; }
        public string VendorPartNum { get; set; }
        // FK references
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual InsertToJob InsertToJob { get; set; }
    }
}
