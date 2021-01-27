using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Job
    {
        public Guid JobId { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        public IEnumerable<MillToolToJob> MillToolToJobs { get; set; }
        public IEnumerable<DrillBitToJob> DrillBitToJobs { get; set; }
    }
}
