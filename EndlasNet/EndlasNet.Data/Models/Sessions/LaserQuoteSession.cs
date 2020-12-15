using System.Collections.Generic;

namespace EndlasNet.Data
{
    public class LaserQuoteSession
    {
        // PK
        public int LaserQuoteSessionId { get; set; }
        // columns
        public bool IsFlowRateAnalytical { get; set; }
        public double FinishedPartWeight { get; set; }
        public int NumLayers { get; set; }
        public int NumParts { get; set; }
        public double PartChangeoverTimeHr { get; set; }
        public double PartSurfaceAreaSqIn { get; set; }
        public double SetupTimeMin { get; set; }
        public double HeatTreatedBlankWt { get; set; }
        public double HeatTreatedPricePerLb { get; set; }
        public double MinHeatTreatmentPrice { get; set; }
        public double ShippingWeightFactor { get; set; }
        public double ArgonCost { get; set; }
        public double EstPowerCost { get; set; }
        public double HourlyLaborRate { get; set; }
        public double HourlyUseRate { get; set; }
        public double FringeRate { get; set; }
        public double ProfitRate { get; set; }
        public double OverheadRate { get; set; }

        // FK references
        // LaserQuoteSession has 0:many raw material-laser quote sessions
        public IEnumerable<RawMaterial_LaserQuoteSession> RawMat_LasQuoteSes { get; set; }
        // LaserQuoteSession has 1 QuoteSession
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}
