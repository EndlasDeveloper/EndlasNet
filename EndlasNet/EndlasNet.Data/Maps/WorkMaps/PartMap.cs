using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class PartMap
    {
        public PartMap(EntityTypeBuilder<Part> entityBuilder)
        {
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
