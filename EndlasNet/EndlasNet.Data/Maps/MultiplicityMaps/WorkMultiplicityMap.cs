using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace EndlasNet.Data
{
    public class WorkMultiplicityMap
    {
        public WorkMultiplicityMap(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);
            // each vendor has 0 to many inserts; each insert has 1 vendor
            modelBuilder.Entity<PartForJob>().HasKey(p => new { p.PartId, p.JobId });

            modelBuilder.Entity<Job>().HasMany(j => j.PartsForJobs).WithOne(p => p.Job);
            modelBuilder.Entity<Part>().HasMany(p => p.PartsForJobs).WithOne(p => p.Part);
            modelBuilder.Entity<Customer>().HasMany(v => v.Jobs).WithOne(i => i.Customer);

            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.ToolsForJobs).WithOne(i => i.Job);
            modelBuilder.Entity<WorkOrder>().HasMany(w => w.ToolsForWorksOrders).WithOne(t => t.WorkOrder);
            // each employee has 0 to many insert to jobs; each insert to job has 1 employee
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Vendors).WithOne(v => v.User);
            modelBuilder.Entity<User>().HasMany(u => u.Powders).WithOne(p => p.User);
            modelBuilder.Entity<User>().HasMany(u => u.Jobs).WithOne(j => j.User);




        }
    }
}
