
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public interface IMachiningToolForWork
    {
        public Guid MachiningToolForWorkId { get; set; }

        public DateTime DateUsed { get; set; }

        public Guid MachiningToolId { get; set; }
        public MachiningTool MachiningTool { get; set; }

        public string Comment { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public MachiningTypes MachiningType { get; set; }
    }
}
