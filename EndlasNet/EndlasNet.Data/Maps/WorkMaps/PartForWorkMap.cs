using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EndlasNet.Data
{
    public class PartForWorkMap
    {

        public PartForWorkMap(EntityTypeBuilder<PartForWorkOrder> entityTypeBuilder)
        {
            // shadow properties
            entityTypeBuilder.Property<DateTime>("CreatedDate");
            entityTypeBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}