using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MultiplicityMap
    {
        public MultiplicityMap(ModelBuilder modelBuilder)
        {
            // Each quote session has 1 customer
            modelBuilder.Entity<QuoteSession>().HasOne(q => q.Customer);
          /*  // Each laser quote session has 1 quote session
            modelBuilder.Entity<LaserQuoteSession>().HasOne(l => l.QuoteSession);
            // Each machine quote session has 1 quote session
            modelBuilder.Entity<MachineQuoteSession>().HasOne(m => m.QuoteSession);
            // each raw material can have 0 to many raw material-laser quote sessions
            modelBuilder.Entity<RawMaterial>().HasMany(r => r.RawMat_LasQuoteSes).WithOne(r => r.RawMaterial);
            // each laser quote session has 0 to many raw material-laser quote sessions
            modelBuilder.Entity<LaserQuoteSession>().HasMany(r => r.RawMat_LasQuoteSes).WithOne(r => r.LaserQuoteSession);*/
        }

    }
}
