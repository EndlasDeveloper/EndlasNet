using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalSnapshot_Job
    {
        public Guid EnvSnapshotId { get; set; }
        public EnvironmentalSnapshot EnvironmentalSnapshot{ get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
    }
}
