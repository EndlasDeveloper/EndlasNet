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
            // each vendor has 0 to many inserts; each insert has 1 vendor
            modelBuilder.Entity<Vendor>().HasMany(v => v.Inserts).WithOne(i => i.Vendor);
            // each employee has 0 to many insert to jobs; each insert to job has 1 employee
            modelBuilder.Entity<User>().HasMany(u => u.InsertToJobs).WithOne(i => i.User);
            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.InsertToJobs).WithOne(i => i.Job);
        }
    }
}
