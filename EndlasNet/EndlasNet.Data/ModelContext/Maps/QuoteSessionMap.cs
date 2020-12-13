using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class QuoteSessionMap
    {
        public QuoteSessionMap(EntityTypeBuilder<QuoteSession> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            entityBuilder.HasKey(q => q.QuoteSessionId);  // set PK
            entityBuilder.Property(q => q.QuoteSessionName).IsRequired();
            entityBuilder.Property(q => q.QuoteSessionDate).IsRequired();
            entityBuilder.Property(q => q.CustomerId).IsRequired();
        }
    }
}
