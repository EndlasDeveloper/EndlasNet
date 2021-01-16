using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class InsertToJob
    {
        public int InsertToJobId { get; set; }
        public DateTime DateUsed { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
