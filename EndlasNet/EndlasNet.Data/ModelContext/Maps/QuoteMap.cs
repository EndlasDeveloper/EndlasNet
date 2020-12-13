using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class QuoteMap
    {
        private Func<EntityTypeBuilder<Quote>> entity;

        public QuoteMap(Func<EntityTypeBuilder<Quote>> entity)
        {
            this.entity = entity;
        }
    }
}
