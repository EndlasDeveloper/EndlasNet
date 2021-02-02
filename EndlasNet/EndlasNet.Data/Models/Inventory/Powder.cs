using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Powder
    {
        public Guid PowderId { get; set; }
        [StringLength(25)]
        public string PowderName { get; set; }
        [StringLength(250)]
        public string VendorDescription { get; set; }
        public string PoNumber { get; set; }
        public DateTime PoDate { get; set; }
        public string BottleNumber { get; set; }

        [Range(1f,1000f)]
        public float Weight { get; set; }

        [Range(0f,float.MaxValue)]
        public float CostPerUnitWeight { get; set; }
        public string LotNumber { get; set; }
        // FK references
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

    }
}
