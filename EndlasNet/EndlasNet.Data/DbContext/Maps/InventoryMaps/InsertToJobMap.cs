using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class InsertToJobMap
    {
        public InsertToJobMap(EntityTypeBuilder<InsertToJob> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(i => i.InsertToJobId);
            // not null
            entityBuilder.Property(i => i.DateUsed).IsRequired();
            entityBuilder.Property(i => i.EmployeeId).IsRequired();
            entityBuilder.Property(i => i.JobId).IsRequired();
        }
    }
}
