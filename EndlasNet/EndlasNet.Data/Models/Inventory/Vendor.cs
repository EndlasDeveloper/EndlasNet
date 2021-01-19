using System;
using System.Collections.Generic;
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
        public IEnumerable<Insert> Inserts { get; set; }

    }
}
