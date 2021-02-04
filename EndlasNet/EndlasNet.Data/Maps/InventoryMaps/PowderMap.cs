using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class PowderMap
    {
        public PowderMap(EntityTypeBuilder<Powder> entityBuilder)
        {
            // PK
            entityBuilder.HasKey(p => p.PowderId);
            // not null
            entityBuilder.Property(p => p.PowderName).IsRequired();
            entityBuilder.Property(p => p.PoNumber).IsRequired();
            entityBuilder.Property(p => p.PoDate).IsRequired();
            entityBuilder.Property(p => p.ParticleSize).IsRequired();
            entityBuilder.Property(p => p.InitWeight).IsRequired();
            entityBuilder.Property(p => p.Weight).IsRequired();
            entityBuilder.Property(p => p.CostPerUnitWeight).IsRequired();
            entityBuilder.Property(p => p.LotNumber).IsRequired();
            entityBuilder.Property(p => p.BottleNumber).IsRequired();


            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
