using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalSnapshot
    {
        // PK
        public Guid EnvSnapshotId { get; set; }
        // columns
        public DateTime DateTimeCollected { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

    }
}
