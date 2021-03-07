using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EndlasNet.Data
{
    public class MachiningToolForWork
    {
        [Key]
        public Guid MachiningToolForWorkId { get; set; }

        [Required]
        [Display(Name = "Date used")]
        public DateTime DateUsed { get; set; }

        [Required]
        [ForeignKey("WorkId")]
        [Display(Name = "Work")]
        public Guid WorkId { get; set; }
        public virtual Work Work { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
