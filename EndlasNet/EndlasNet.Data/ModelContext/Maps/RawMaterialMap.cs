using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class RawMaterialMap
    {
        public RawMaterialMap(EntityTypeBuilder<RawMaterial> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            entityBuilder.HasKey(r => r.RawMaterialId);  // set PK
            // not null
            entityBuilder.Property(r => r.PowderType).IsRequired();
            entityBuilder.Property(r => r.PowderLayerPrice).IsRequired();
            entityBuilder.Property(r => r.CladLayerDensity).IsRequired();
            entityBuilder.Property(r => r.Vendor).IsRequired();
            entityBuilder.Property(r => r.OrderNumber).IsRequired();
            entityBuilder.Property(r => r.FlowRateSlope).IsRequired();
            entityBuilder.Property(r => r.FlowRateYIntercept).IsRequired();
        }
    }
}
