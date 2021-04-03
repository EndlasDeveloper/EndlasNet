using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class CreatedUpdatedDateMap
    {

        public CreatedUpdatedDateMap(ModelBuilder modelBuilder) 
        {
            // MACHINING TOOL
            modelBuilder.Entity<MachiningTool>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<MachiningTool>().Property<DateTime>("UpdatedDate");
            // PowderBottle
            modelBuilder.Entity<PowderBottle>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<PowderBottle>().Property<DateTime>("UpdatedDate");

            // WORK
            modelBuilder.Entity<Work>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Work>().Property<DateTime>("UpdatedDate");

            // PART FOR JOB
            modelBuilder.Entity<PartForWork>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<PartForWork>().Property<DateTime>("UpdatedDate");

            // MACHINING TOOL FOR WORK
            modelBuilder.Entity<MachiningToolForWork>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<MachiningToolForWork>().Property<DateTime>("UpdatedDate");
        }

    }
}
