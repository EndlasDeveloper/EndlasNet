using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Vendor
    {
        [Key]
        public Guid VendorId { get; set; }

        [Required]
        [Display(Name = "Vendor name")]
        public string VendorName { get; set; }

        [Required]
        [Display(Name = "Point of contact")]
        public string PointOfContact { get; set; }

        [Required]
        [Display(Name = "Vendor address")]
        public string VendorAddress { get; set; }

        [Required]
        [Display(Name = "Vendor phone")]
        [DataType(DataType.PhoneNumber)]
        public string VendorPhone { get; set; }

        public IEnumerable<MachiningTool> MachiningTools{ get; set; }

    }
}
