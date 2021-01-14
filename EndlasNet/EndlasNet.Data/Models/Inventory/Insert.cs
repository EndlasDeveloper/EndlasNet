using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Insert
    {
        public int InsertId { get; set; }
        public string PurchaseOrderNum { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public float PurchaseOrderPrice { get; set; }
        public string Description { get; set; }
        public string VendorPartNum { get; set; }
        public float ToolTipRadius { get; set; }
        public string PurchaseDescription { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public int? InsertToJobId { get; set; }
        public virtual InsertToJob InsertToJob { get; set; }
    }
}
