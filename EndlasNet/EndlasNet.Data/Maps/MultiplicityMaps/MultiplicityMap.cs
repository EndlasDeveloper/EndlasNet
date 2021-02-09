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
            modelBuilder.Entity<Customer>().HasMany(v => v.Jobs).WithOne(i => i.Customer);
            // each job has 0 to many tool to to jobs (i.e. each job uses 0 to many tools); each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.ToolsForJobs).WithOne(i => i.Job);

            /*** USER ***/
            // each employee has (could create) 0 to many insert to jobs; each insert to job has 1 employee that created it
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User);
            // each user has (could create) 0 to many vendors; each vendor has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Vendors).WithOne(v => v.User);
            // each user has (could create) 0 to many powders; each powder has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Powders).WithOne(p => p.User);
            // each user has (could create) 0 to many jobs; each job has a user that created it 
            modelBuilder.Entity<User>().HasMany(u => u.Jobs).WithOne(j => j.User);


            /*** WORK ***/
            // each job has 0 to many parts for jobs (parts assigned to existing jobs); each part for job has exactly one job
            modelBuilder.Entity<Job>().HasMany(j => j.PartsForJobs).WithOne(p => p.Job);
            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(j => j.ToolsForJobs).WithOne(m => m.Job);
            // each WO has 0 to many machining tools for WOs (similar to job-machining tools for jobs); each tool for work order has 1 WO
            modelBuilder.Entity<WorkOrder>().HasMany(w => w.ToolsForWorksOrders).WithOne(m => m.WorkOrder);

            // each part has 0 to many parts for jobs (assignments to existing jobs); each part for job references exactly one part
            modelBuilder.Entity<Part>().HasMany(p => p.PartsForJobs).WithOne(p => p.Part);
            // each customer has 0 to many jobs; each job has exactly 1 customer
            modelBuilder.Entity<Customer>().HasMany(c => c.Jobs).WithOne(j => j.Customer);



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
