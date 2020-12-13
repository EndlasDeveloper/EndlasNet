using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachineQuoteSessionMap
    {
        private EntityTypeBuilder<MachineQuoteSession> entityTypeBuilder;

        public MachineQuoteSessionMap(EntityTypeBuilder<MachineQuoteSession> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}
