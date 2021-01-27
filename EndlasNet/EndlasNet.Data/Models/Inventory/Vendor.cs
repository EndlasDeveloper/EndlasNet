using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Vendor
    {
        public Guid VendorId { get; set; }
        // columns
        public string VendorName { get; set; }
        public string PointOfContact { get; set; } 

        public string VendorAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string VendorPhone { get; set; }

        public virtual User User { get; set; }
        public IEnumerable<Insert> Inserts { get; set; }

    }
}
