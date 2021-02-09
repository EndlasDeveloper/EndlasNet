using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkOrderMap
    {

        public MachiningToolForWorkOrderMap(EntityTypeBuilder<MachiningToolForWorkOrder> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(m => m.MachiningToolForJobId);
            // not null
            entityTypeBuilder.Property(m => m.DateUsed).IsRequired();
            // shadow properties
            entityTypeBuilder.Property<DateTime>("CreatedDate");
            entityTypeBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}