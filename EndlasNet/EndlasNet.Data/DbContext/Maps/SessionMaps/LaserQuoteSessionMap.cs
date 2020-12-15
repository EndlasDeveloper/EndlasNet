using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class LaserQuoteSessionMap
    {
        public LaserQuoteSessionMap(EntityTypeBuilder<LaserQuoteSession> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(l => l.LaserQuoteSessionId);
            // not null
            entityBuilder.Property(l => l.QuoteSessionId).IsRequired();
            entityBuilder.Property(l => l.IsFlowRateAnalytical).IsRequired();
            entityBuilder.Property(l => l.FinishedPartWeight).IsRequired();
            entityBuilder.Property(l => l.NumLayers).IsRequired();
            entityBuilder.Property(l => l.NumParts).IsRequired();
            entityBuilder.Property(l => l.PartChangeoverTimeHr).IsRequired();
            entityBuilder.Property(l => l.PartSurfaceAreaSqIn).IsRequired();
            entityBuilder.Property(l => l.SetupTimeMin).IsRequired();
            entityBuilder.Property(l => l.ShippingWeightFactor).IsRequired();
            entityBuilder.Property(l => l.ArgonCost).IsRequired();
            entityBuilder.Property(l => l.EstPowerCost).IsRequired();
            entityBuilder.Property(l => l.HourlyLaborRate).IsRequired();
            entityBuilder.Property(l => l.HourlyUseRate).IsRequired();
            entityBuilder.Property(l => l.FringeRate).IsRequired();
            entityBuilder.Property(l => l.ProfitRate).IsRequired();
            entityBuilder.Property(l => l.OverheadRate).IsRequired();
        }
    }
}