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

            // *** There will be more of these in the future *****

        }
    }
}