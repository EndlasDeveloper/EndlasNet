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
            // set PK
            entityBuilder.HasKey(p => p.PartId);
            entityBuilder.Property(p => p.DrawingNumber).IsRequired();
            entityBuilder.Property(p => p.ConditionDescription).IsRequired();
            entityBuilder.Property(p => p.InitWeight).IsRequired();
            entityBuilder.Property(p => p.Weight).IsRequired();
            entityBuilder.Property(p => p.CladdedWeight).IsRequired();
            entityBuilder.Property(p => p.ProcessingNotes).IsRequired();
            entityBuilder.Property(p => p.ImageName).IsRequired();
            entityBuilder.Property(p => p.DrawingImage).IsRequired();

            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
