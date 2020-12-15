using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
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