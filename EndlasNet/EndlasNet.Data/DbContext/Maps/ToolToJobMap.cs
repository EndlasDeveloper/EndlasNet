using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class ToolToJobMap
    {
        public ToolToJobMap(EntityTypeBuilder<ToolToJob> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(t => t.ToolToJobId);
            // not null
            entityBuilder.Property(i => i.DateUsed).IsRequired();
            entityBuilder.Property(i => i.JobId).IsRequired();
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
