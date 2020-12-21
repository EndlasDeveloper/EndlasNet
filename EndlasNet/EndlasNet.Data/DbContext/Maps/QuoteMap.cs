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
            // not null
            entityBuilder.Property(q => q.PowderDirectTotal).IsRequired();
            entityBuilder.Property(q => q.GasTotal).IsRequired();
            entityBuilder.Property(q => q.EnergyTotal).IsRequired();
            entityBuilder.Property(q => q.ShippingTotal).IsRequired();
            entityBuilder.Property(q => q.CogsTotal).IsRequired();
            entityBuilder.Property(q => q.LaborDirectTotal).IsRequired();
            entityBuilder.Property(q => q.FringeTotal).IsRequired();
            entityBuilder.Property(q => q.ProfitTotal).IsRequired();
            entityBuilder.Property(q => q.OverheadTotal).IsRequired();
        }
    }
}