using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Job
    {
        // PK
        public Guid JobId { get; set; }
        // Job has 1:Many Environmental Snapshots
        public IEnumerable<EnvironmentalSnapshot_Job> EnvironmentalSnapshot_Jobs { get; set; }
        // Job has 0:Many InsertToJobs
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        // Job has 0:Many MillToolToJobs
        public IEnumerable<MillToolToJob> MillToolToJobs { get; set; }
        // Job has 0:Many DrillBitToJobs
        public IEnumerable<DrillBitToJob> DrillBitToJobs { get; set; }
    }
}
