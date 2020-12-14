using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class MachineQuoteSessionMap
    {
        public MachineQuoteSessionMap(EntityTypeBuilder<MachineQuoteSession> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(m => m.MachineQuoteSessionId);
            // not null
            entityBuilder.Property(l => l.QuoteSessionId).IsRequired();

            // *** There will be more of these in the future *****

        }
    }
}