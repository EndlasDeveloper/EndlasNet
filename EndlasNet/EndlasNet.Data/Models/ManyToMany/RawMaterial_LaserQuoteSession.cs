namespace EndlasNet.Data
{
    // Fixes many to many relationship between raw material and laser quote session
    public class RawMaterial_LaserQuoteSession
    {
        // Composite PK made of FK from raw mat and laser quote session
        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int LaserQuoteSessionId { get; set; }
        public LaserQuoteSession LaserQuoteSession { get; set; }

        // columns
        public double PowerInWatts { get; set; }
        public double AvgThicknessIn { get; set; }
        public double SpotSizeMm { get; set; }
        public double PercentBeadOverlap { get; set; }
        public double SurfaceVelocityMmPerSec { get; set; }
        public double PowderRpm { get; set; }
        public double EstCaptureEffeciency { get; set; }
        public double ProcessingFlowRateLiPerMin { get; set; }
        public double LayerSurfaceAreaSqIn { get; set; }

    }
}
