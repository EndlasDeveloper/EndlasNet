﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class MachiningToolForWork : IMachiningToolForWork
    {
        [Key]
        public Guid MachiningToolForWorkId { get; set; }

        [Required]
        [Display(Name = "Date used")]
        public DateTime DateUsed { get; set; } = DateTime.Now;

        [ForeignKey("WorkItemId")]
        [Display(Name ="Work item")]
        public Guid? WorkItemId { get; set; }
        public virtual WorkItem WorkItem { get; set; }
        [Required]
        [ForeignKey("MachiningToolId")]
        [Display(Name = "Machining tool")]
        public Guid MachiningToolId { get; set; }
        public virtual MachiningTool MachiningTool { get; set; }

        [Display(Name ="Tool work comment")]
        public string Comment { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name ="Machining type")]
        public MachiningTypes MachiningType { get; set; }


    }
}
