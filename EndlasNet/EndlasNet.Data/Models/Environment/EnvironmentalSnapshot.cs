using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalSnapshot
    {
        // PK
        [Key]
        public Guid EnvSnapshotId { get; set; }
        // columns
        [Required]
        [Display(Name = "Time collected")]
        public DateTime DateTimeCollected { get; set; }

        [Required]
        [Display(Name = "Temperature")]
        public float Temperature { get; set; }

        [Required]
        [Display(Name = "Humidity")]
        public float Humidity { get; set; }

    }
}
