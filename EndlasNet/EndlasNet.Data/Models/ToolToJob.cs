using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class ToolToJob
    {
        public Guid ToolToJobId { get; set; }
        public DateTime DateUsed { get; set; }
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
