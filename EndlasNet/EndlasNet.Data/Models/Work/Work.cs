﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Work
    {
        [Key]
        public Guid WorkId { get; set; }
        
        [Required]
        [Display(Name ="Endlas number")]
        public string EndlasNumber { get; set; }

        [Required]
        [Display(Name = "Work description")]
        public string WorkDescription { get; set; }
        [Required]
        [Display(Name = "Status")]
        public Status Status { get; set; }

        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }

        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public IEnumerable<PartForWork> PartsForWork { get; set; }
        public IEnumerable<MachiningToolForWork> ToolsForWork { get; set; }
    }
}
