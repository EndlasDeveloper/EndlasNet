using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    /*
    * Class: RawMaterial_LaserQuoteSessionMap
    * Description: Map object to describe the column constraints in RawMaterial_LaserQuoteSession entity
    */
    public class RawMaterial_LaserQuoteSessionMap
    {
        public RawMaterial_LaserQuoteSessionMap(EntityTypeBuilder<RawMaterial_LaserQuoteSession> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            // define composite primary key
            entityBuilder.HasKey(rl => new { rl.LaserQuoteSessionId, rl.RawMaterialId });
            // not nulls
            entityBuilder.Property(rl => rl.PowerInWatts).IsRequired();
            entityBuilder.Property(rl => rl.AvgThicknessIn).IsRequired();
            entityBuilder.Property(rl => rl.SpotSizeMm).IsRequired();
            entityBuilder.Property(rl => rl.PercentBeadOverlap).IsRequired();
            entityBuilder.Property(rl => rl.SurfaceVelocityMmPerSec).IsRequired();
            entityBuilder.Property(rl => rl.PowderRpm).IsRequired();
            entityBuilder.Property(rl => rl.EstCaptureEffeciency).IsRequired();
            entityBuilder.Property(rl => rl.ProcessingFlowRateLiPerMin).IsRequired();
            entityBuilder.Property(rl => rl.LayerSurfaceAreaSqIn).IsRequired();

        }

    }
}
