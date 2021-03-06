using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Data
{
    public class MultiplicityMap
    {
        public MultiplicityMap(ModelBuilder modelBuilder)
        {
            /*** INVENTORY ***/
            // each vendor has 0 to many inserts; each insert has 1 vendor
            modelBuilder.Entity<Vendor>().HasMany(v => v.MachiningTools).WithOne(i => i.Vendor);
            // each customer has many jobs; each job has 1 customer
            modelBuilder.Entity<Customer>().HasMany(v => v.Work).WithOne(i => i.Customer);

            modelBuilder.Entity<Work>().HasMany(v => v.ToolsForWork).WithOne(t => t.Work);
            /*** USER ***/
            // each employee has (could create) 0 to many insert to jobs; each insert to job has 1 employee that created it
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User);
            // each user has (could create) 0 to many vendors; each vendor has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Vendors).WithOne(v => v.User);
            // each user has (could create) 0 to many powders; each powder has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Powders).WithOne(p => p.User);
            // each user has (could create) 0 to many jobs; each job has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Work).WithOne(j => j.User);
            //modelBuilder.Entity<User>().HasMany(u => u.StaticPartInfo).WithOne(s => s.User);

            /*** WORK ***/
            // each job has 0 to many parts for jobs (parts assigned to existing jobs); each part for job has exactly one job
            modelBuilder.Entity<Work>().HasMany(j => j.PartsForWork).WithOne(p => p.Work);
            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Work>().HasMany(j => j.ToolsForWork).WithOne(m => m.Work);
            // each WO has 0 to many machining tools for WOs (similar to job-machining tools for jobs); each tool for work order has 1 WO

            // each customer has 0 to many jobs; each job has exactly 1 customer
            modelBuilder.Entity<Customer>().HasMany(c => c.Work).WithOne(j => j.Customer);
           
            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.LineItems).WithOne(l => l.StaticPowderInfo);
            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.Powders).WithOne(l => l.StaticPowderInfo);


            /*** QUOTE ***/
            // COMMENTED OUT FOR NOW. INTRODUCE LATER WHEN TRYING TO WORK WITH QUOTES
            /*
            // each raw material can have 0 to many raw material-laser quote sessions
            modelBuilder.Entity<RawMaterial>().HasMany(r => r.RawMat_LasQuoteSes).WithOne(r => r.RawMaterial);
            // each laser quote session has 0 to many raw material-laser quote sessions
            modelBuilder.Entity<LaserQuoteSession>().HasMany(l => l.RawMat_LasQuoteSes).WithOne(r => r.LaserQuoteSession);
            */

        }
    }
}
