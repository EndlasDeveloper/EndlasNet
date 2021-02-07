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
        [Display(Name = "Vendor name")]
        public string VendorName { get; set; }
        [Display(Name = "Point of contact")]
        public string PointOfContact { get; set; }
        [Display(Name = "Vendor address")]
        public string VendorAddress { get; set; }
        [Display(Name = "Vendor phone")]
        [DataType(DataType.PhoneNumber)]
        public string VendorPhone { get; set; }
        [Display(Name = "User")]
        public virtual User User { get; set; }
        public IEnumerable<MachiningTool> MachiningTools{ get; set; }

    }
}
