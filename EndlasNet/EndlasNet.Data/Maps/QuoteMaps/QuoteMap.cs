using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    /*
    * Class: QuoteMap
    * Description: Map object to describe the column constraints in Quote entity
    */
    public class QuoteMap
    {
        public QuoteMap(EntityTypeBuilder<Quote> entityBuilder)
        {
            // make .NET happpy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(q => q.QuoteId);

        }
    }
}