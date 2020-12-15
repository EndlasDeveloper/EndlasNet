using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class OptionalLaserServicesMap
    {
        public OptionalLaserServicesMap(EntityTypeBuilder<OptionalLaserService> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(o => o.OptionalLaserServicesId);
            // not null
            entityBuilder.Property(o => o.HeatTreatedBlankWt).IsRequired();
            entityBuilder.Property(o => o.HeatTreatedPricePerLb).IsRequired();
            entityBuilder.Property(o => o.MinHeatTreatmentPrice).IsRequired();
        }
    }
}

