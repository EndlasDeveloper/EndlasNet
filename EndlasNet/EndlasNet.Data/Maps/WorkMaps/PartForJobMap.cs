using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{ 
    public class PartForJobMap
    {
        public PartForJobMap(EntityTypeBuilder<PartForJob> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(p => p.PartForWorkId);
     
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
