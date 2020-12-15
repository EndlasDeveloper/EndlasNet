using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class IntermediateParamMap
    {
        public IntermediateParamMap(EntityTypeBuilder<IntermediateParam> entityBuilder)
        {
            // make .NET happpy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(i => i.IntermediateParamId);
            // not null
            entityBuilder.Property(i => i.SurfaceVelocity).IsRequired();
            entityBuilder.Property(i => i.StepMm).IsRequired();
            entityBuilder.Property(i => i.StepIn).IsRequired();
            entityBuilder.Property(i => i.AssumedAvgPassLenIn).IsRequired();
            entityBuilder.Property(i => i.PseudoWidthIn).IsRequired();
            entityBuilder.Property(i => i.PseudoNumPasses).IsRequired();
            entityBuilder.Property(i => i.TimePerBeadSec).IsRequired();
            entityBuilder.Property(i => i.TimeBetweenBeadsMin).IsRequired();
            entityBuilder.Property(i => i.TimePerLayerMin).IsRequired();
            entityBuilder.Property(i => i.CladAddRateSqInPerMin).IsRequired();
            entityBuilder.Property(i => i.ApproxVolPerLayerCubicCm).IsRequired();
            entityBuilder.Property(i => i.LaserQuoteSessionId).IsRequired();
        }
    }
}
