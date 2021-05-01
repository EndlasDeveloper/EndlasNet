using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{

    public class MachiningTool
    {
        [Key]
        public Guid MachiningToolId { get; set; }

        [Required]
        [Display(Name = "Tool type")]
        public ToolTypes ToolType { get; set; }

        [Required]
        [Display(Name = "Tool measurment value")]
        public float ToolDiameter { get; set; }

        [Required]
        [Display(Name ="Tool measurment type")]
        public MachiningToolMetrics RadialMetric { get; set; }

        [Required]
        [Display(Name ="Tool measurment units")]
        public MachiningToolUnits Units { get; set; }

        [Required]
        [Display(Name = "Tool description")]
        public string ToolDescription { get; set; }

        [Required]
        [Display(Name ="Vendor ID")]
        public string VendorDescription { get; set; }

        [Range(1, 10000)]
        [Display(Name = "Initial tool count")]
        [Required]
        public int InitToolCount { get; set; }

        [Range(1, 10000)]
        [Display(Name = "Tool count")]
        [Required]
        public int ToolCount { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "PO number")]
        public string PurchaseOrderNum { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "PO date")]
        public DateTime PurchaseOrderDate { get; set; }

        [Required]
        [Range(0f, 10000.0f)]
        [DataType(DataType.Currency)]
        [Display(Name = "Batch PO cost")]
        public float PurchaseOrderCost { get; set; }

        [Required]
        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        // FK references
        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VendorId")]
        [Display(Name = "Vendor")]
        public Guid? VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        [NotMapped]
        public string DropDownDisplayReference { get; set; }
    }
}
