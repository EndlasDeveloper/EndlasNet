using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{ 
    public class Job
    {
        public int JobId { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
    }
}
