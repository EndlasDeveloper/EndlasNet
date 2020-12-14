using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class RawMaterial_LaserQuoteSessionMap
    {
        public RawMaterial_LaserQuoteSessionMap(EntityTypeBuilder<RawMaterial_LaserQuoteSession> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            // define composite primary key
            entityBuilder.HasKey(rl => new { rl.LaserQuoteSessionId, rl.RawMaterialId });       
        }

    }
}
