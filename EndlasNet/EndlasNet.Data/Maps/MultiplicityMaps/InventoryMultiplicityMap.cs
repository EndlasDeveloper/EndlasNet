using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace EndlasNet.Data
{
    public class InventoryMultiplicityMap
    {
        public InventoryMultiplicityMap(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);
            // each vendor has 0 to many inserts; each insert has 1 vendor
            modelBuilder.Entity<Vendor>().HasMany(v => v.MachiningTools).WithOne(i => i.Vendor);

            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.InsertToJobs).WithOne(i => i.Job);

            // each employee has 0 to many insert to jobs; each insert to job has 1 employee
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Vendors).WithOne(v => v.User);
            modelBuilder.Entity<User>().HasMany(u => u.Powders).WithOne(p => p.User);
            modelBuilder.Entity<User>().HasMany(u => u.Jobs).WithOne(j => j.User);




        }
    }
}
