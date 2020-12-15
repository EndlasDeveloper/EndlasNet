using System.Collections.Generic;

namespace EndlasNet.Data
{
    public class RawMaterial
    {
        // PK
        public int RawMaterialId { get; set; }
        // columns
        public string PowderType { get; set; }
        public double PowderLayerPrice { get; set; }
        public double CladLayerDensity { get; set; }
        public string Vendor { get; set; }
        public string OrderNumber { get; set; }
        public string PowderFeeder { get; set; }

        // FK reference : has 0 to many RawMaterial_LaserQuoteSessions (artiface of many to many relation between raw mat and laser session)
        public IEnumerable<RawMaterial_LaserQuoteSession> RawMat_LasQuoteSes { get; set; }
        public int? RawMaterialEmpiricalId { get; set; }
        public virtual RawMaterialEmpirical RawMaterialEmpirical { get; set; }
    }
}
