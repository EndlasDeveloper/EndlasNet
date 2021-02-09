using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalSnapshotMap
    {
        public EnvironmentalSnapshotMap(EntityTypeBuilder<EnvironmentalSnapshot> entityBuilder)
        {
            entityBuilder.HasKey(e => e.EnvSnapshotId);
            // not null
            entityBuilder.Property(e => e.Temperature).IsRequired();
            entityBuilder.Property(e => e.Humidity).IsRequired();
            entityBuilder.Property(e => e.DateTimeCollected).IsRequired();          
        }
    }
}
