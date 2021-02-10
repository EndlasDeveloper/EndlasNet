using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EndlasNet.Data
{
    public class PartForWorkOrderMap
    {

        public PartForWorkOrderMap(EntityTypeBuilder<PartForWorkOrder> entityTypeBuilder)
        {
            // set PK
            entityTypeBuilder.HasKey(p => p.PartForWorkOrderId);

            // shadow properties
            entityTypeBuilder.Property<DateTime>("CreatedDate");
            entityTypeBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}