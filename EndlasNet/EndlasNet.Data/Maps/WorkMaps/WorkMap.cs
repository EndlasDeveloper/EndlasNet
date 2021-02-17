using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkMap
    {
        public WorkMap(EntityTypeBuilder<Work> entityBuilder)
        {
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
