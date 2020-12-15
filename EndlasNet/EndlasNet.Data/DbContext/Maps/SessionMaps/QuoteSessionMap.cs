using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    public class QuoteSessionMap
    {
        public QuoteSessionMap(EntityTypeBuilder<QuoteSession> entityBuilder)
        {
            // make .NET happy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(q => q.QuoteSessionId); 
            // not null
            entityBuilder.Property(q => q.QuoteSessionName).IsRequired();
            entityBuilder.Property(q => q.QuoteSessionDate).IsRequired();
        }
    }
}
