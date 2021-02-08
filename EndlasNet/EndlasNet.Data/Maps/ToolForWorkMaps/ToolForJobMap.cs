using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class ToolForJobMap
    {
        public ToolForJobMap(EntityTypeBuilder<MachiningToolForJob> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(t => t.MachiningToolForJobId);
            // not null
            entityBuilder.Property(i => i.DateUsed).IsRequired();
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
