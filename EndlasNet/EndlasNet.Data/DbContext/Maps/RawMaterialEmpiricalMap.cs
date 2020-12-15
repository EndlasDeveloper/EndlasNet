﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class RawMaterialEmpiricalMap
    {
        public RawMaterialEmpiricalMap(EntityTypeBuilder<RawMaterialEmpirical> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(r => r.RawMaterialEmpiricalId);
            // not null
            entityBuilder.Property(r => r.FlowRateSlope).IsRequired();
            entityBuilder.Property(r => r.FlowRateYIntercept).IsRequired();
        }
    }
}
