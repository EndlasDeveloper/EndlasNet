using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace EndlasNet.Data
{
    public class EnvironmentalMultiplicityMap
    {
        public EnvironmentalMultiplicityMap(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);
/*            modelBuilder.Entity<EnvironmentalSnapshot_Job>().HasKey(e => new { e.EnvSnapshotId, e.JobId });
            // each vendor has 0 to many inserts; each insert has 1 vendor
            modelBuilder.Entity<EnvironmentalSnapshot>().HasMany(e => e.EnvironmentalSnapshot_Jobs).WithOne(e => e.EnvironmentalSnapshot);
            modelBuilder.Entity<Job>().HasMany(e => e.EnvironmentalSnapshot_Jobs).WithOne(e => e.Job);*/

            // each job has 0 to many insert to jobs; each insert to job has 1 job
            modelBuilder.Entity<Job>().HasMany(v => v.InsertToJobs).WithOne(i => i.Job).OnDelete(DeleteBehavior.ClientNoAction);

            // each employee has 0 to many insert to jobs; each insert to job has 1 employee
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Powders).WithOne(v => v.User);

            modelBuilder.Entity<User>().HasMany(u => u.Jobs).WithOne(v => v.User);



        }
    }
}
