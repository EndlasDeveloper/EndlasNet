using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalSnapshot
    {
        // PK
        public Guid EnvSnapshotId { get; set; }
        // columns
        [Display(Name = "Time collected")]
        public DateTime DateTimeCollected { get; set; }
        [Display(Name = "Temperature")]
        public float Temperature { get; set; }
        [Display(Name = "Humidity")]
        public float Humidity { get; set; }

    }
}
