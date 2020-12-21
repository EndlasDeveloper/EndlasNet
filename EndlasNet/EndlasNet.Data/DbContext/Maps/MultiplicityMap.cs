using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace EndlasNet.Data
{
    /*
    * Class: MultiplicityMap
    * Description: Map object to describe multiplicities between entities if it isn't implied in the class structure
    */
    public class MultiplicityMap
    {
        public MultiplicityMap(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);
            // each raw material can have 0 to many raw material-laser quote sessions
            modelBuilder.Entity<RawMaterial>().HasMany(r => r.RawMat_LasQuoteSes).WithOne(r => r.RawMaterial);
            // each laser quote session has 0 to many raw material-laser quote sessions
            modelBuilder.Entity<LaserQuoteSession>().HasMany(l => l.RawMat_LasQuoteSes).WithOne(r => r.LaserQuoteSession);
        }
    }
}
