﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WorkId { get; set; }
        
        [Required]
        [Display(Name ="Endlas number")]
        public string EndlasNumber { get; set; }

        [ForeignKey("QuoteId")]
        [Display(Name ="Quote")]
        public Guid? QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

        [Required]
        [Display(Name = "Work description")]
        public string WorkDescription { get; set; }
        [Required]
        [Display(Name = "Status")]
        public WorkStatus Status { get; set; }

        [Display(Name = "Purchase order number")]
        public string PurchaseOrderNum { get; set; }

        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }


        [Display(Name ="PO date")]
        public DateTime? PoDate { get; set; }


        [ForeignKey("CustomerId")]
        [Display(Name ="Customer")]
        public Guid? CustomerId { get; set; }
        [Display(Name ="Customer")]
        public virtual Customer Customer { get; set; }


        /*********************** PDF ***************************/
        [NotMapped]
        [Display(Name = "Process sheet notes")]
        public IFormFile ProcessSheetNotesFile{ get; set; }
        public byte[] ProcessSheetNotesPdfBytes { get; set; }

        /*******************************************************/

        [NotMapped]
        [Display(Name = "Clear process sheet notes")]
        public bool ClearPdf { get; set; }

        [NotMapped]
        public string WorkType { get; set; }

        [NotMapped]
        [Display(Name ="Number of work items")]
        public int NumWorkItems { get; set; }

        public IEnumerable<WorkItem> WorkItems { get; set; }
        public IEnumerable<MachiningToolForWork> MachiningToolsForWork { get; set; }

    }
}
