using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class RawMaterialMap
    {
        public RawMaterialMap(EntityTypeBuilder<RawMaterial> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(r => r.RawMaterialId);  
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
