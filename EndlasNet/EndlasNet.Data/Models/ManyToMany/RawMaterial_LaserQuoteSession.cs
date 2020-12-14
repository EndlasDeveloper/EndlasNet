namespace EndlasNet.Data
{
    // Fixes many to many relationship between raw material and laser quote session
    public class RawMaterial_LaserQuoteSession
    {
        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int LaserQuoteSessionId { get; set; }
        public LaserQuoteSession LaserQuoteSession { get; set; }
    }
}
