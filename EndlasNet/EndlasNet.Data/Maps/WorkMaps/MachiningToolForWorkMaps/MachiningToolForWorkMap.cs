using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkMap
    {

        public MachiningToolForWorkMap(EntityTypeBuilder<MachiningToolForWork> entityTypeBuilder)
        {
            // shadow properties
            entityTypeBuilder.Property<DateTime>("CreatedDate");
            entityTypeBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}