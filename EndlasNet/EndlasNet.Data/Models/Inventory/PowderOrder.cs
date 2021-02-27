using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class PowderOrder
    {
        [Key]
        public Guid PowderOrderId { get; set; }

        [Required]
        [Display(Name ="Purchase order number")]
        public string PurchaseOrderNum { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name ="Purchase order date")]
        public DateTime PurchaseOrderDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Shipping cost")]
        public float? ShippingCost { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Tax cost")]
        public float? TaxCost { get; set; }

        [ForeignKey("VendorId")]
        public Guid VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public IEnumerable<LineItem> LineItems { get; set; }
    }
}
