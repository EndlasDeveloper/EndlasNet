using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Vendor
    {
        public int VendorId { get; set; }
        // columns
        public string VendorName { get; set; }
        public string PointOfContact { get; set; } 

        public string VendorAddress { get; set; }
        public string VendorPhone { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User User { get; set; }
        public IEnumerable<Insert> Inserts { get; set; }

    }
}
