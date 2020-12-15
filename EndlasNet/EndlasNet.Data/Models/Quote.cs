namespace EndlasNet.Data
{
    public class Quote
    {
        // PK
        public int QuoteId { get; set; }
        // columns
        public double PowderDirectTotal { get; set; }
        public double GasTotal { get; set; }
        public double EnergyTotal { get; set; }
        public double ShippingTotal { get; set; }
        public double CogsTotal { get; set; }
        public double LaborDirectTotal { get; set; }
        public double FringeTotal { get; set; }
        public double ProfitTotal { get; set; }
        public double OverheadTotal { get; set; }

        // FK reference
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}