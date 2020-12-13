using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class LaserQuoteSessionMap
    {
        public LaserQuoteSessionMap(EntityTypeBuilder<LaserQuoteSession> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            entityBuilder.HasKey(l => l.LaserQuoteSessionId);  // set PK
            // not null
            entityBuilder.Property(l => l.QuoteSessionId).IsRequired();
        }
    }
}
