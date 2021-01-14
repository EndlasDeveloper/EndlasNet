using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class Vendor
    {
        public int VendorId { get; set; }
        // columns
        public string POC { get; set; } // Point Of Contact
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Insert> Inserts { get; set; }

    }
}
