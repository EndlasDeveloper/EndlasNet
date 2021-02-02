using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    // Joining table for EnvSnapshot and Job
    public class EnvironmentalSnapshot_Job
    {
        // composite PK, FK references
        public Guid EnvSnapshotId { get; set; }
        public EnvironmentalSnapshot EnvironmentalSnapshot{ get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
