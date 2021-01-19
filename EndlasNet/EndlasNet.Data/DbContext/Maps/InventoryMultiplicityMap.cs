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
            modelBuilder.Entity<Vendor>().HasMany(v => v.Inserts).WithOne(i => i.Vendor);
            // each employee has 0 to many insert to jobs; each insert to job has 1 employee
            modelBuilder.Entity<User>().HasMany(u => u.InsertToJobs).WithOne(i => i.User);
            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.InsertToJobs).WithOne(i => i.Job);
        }
    }
}
