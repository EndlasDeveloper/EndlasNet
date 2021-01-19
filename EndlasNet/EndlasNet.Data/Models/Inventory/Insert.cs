using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Insert
    {
        // PK
        [ForeignKey("VendorId")]
        public int InsertId { get; set; }
        // columns
        public string PurchaseOrderNum { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public float PurchaseOrderPrice { get; set; }
        public string Description { get; set; }
        public string VendorPartNum { get; set; }
        public float ToolTipRadius { get; set; }
        public string PurchaseDescription { get; set; }
        // FK references
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual InsertToJob InsertToJob { get; set; }
    }
}
