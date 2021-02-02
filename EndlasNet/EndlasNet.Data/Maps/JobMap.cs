using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class JobMap
    {
        public JobMap(EntityTypeBuilder<Job> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(j => j.JobId);
            entityBuilder.Property(j => j.EndlasNumber).IsRequired();
            entityBuilder.Property(j => j.JobDescription).IsRequired();
            entityBuilder.Property(j => j.DueDate).IsRequired();

            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
