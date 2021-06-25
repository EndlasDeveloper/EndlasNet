using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Data
{
    public class MultiplicityMap
    {
        public MultiplicityMap(ModelBuilder modelBuilder)
        {
            /*** INVENTORY ***/
            modelBuilder.Entity<Vendor>().HasMany(v => v.MachiningTools).WithOne(i => i.Vendor).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>().HasMany(v => v.Work).WithOne(i => i.Customer).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.LineItems).WithOne(l => l.StaticPowderInfo).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.Powders).WithOne(l => l.StaticPowderInfo).OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<PowderBottle>().HasMany(p => p.PowderForParts).WithOne(p => p.PowderBottle).OnDelete(DeleteBehavior.SetNull);

            /*** USER ***/
            modelBuilder.Entity<User>().HasMany(u => u.PowderBottles).WithOne(p => p.User).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().HasMany(u => u.Work).WithOne(j => j.User).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().HasMany(u => u.PowderForParts).WithOne(j => j.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(u => u.MachiningToolForJobs).WithOne(j => j.User).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>().HasMany(u => u.MachiningToolForWorkOrders).WithOne(j => j.User).OnDelete(DeleteBehavior.NoAction);


            /*** WORK ***/
            modelBuilder.Entity<PartForWork>().HasMany(p => p.PowdersUsed).WithOne(p => p.PartForWork).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StaticPartInfo>().HasMany(s => s.WorkItems).WithOne(w => w.StaticPartInfo).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Work>().HasMany(w => w.WorkItems).WithOne(w => w.Work).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkItem>().HasMany(w => w.PartsForWork).WithOne(p => p.WorkItem).OnDelete(DeleteBehavior.Cascade);

            /*** ACTION ***/
            modelBuilder.Entity<Work>().HasMany(j => j.ToolsForJob).WithOne(m => m.Work).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Work>().HasMany(j => j.ToolsForWorkOrder).WithOne(m => m.Work).OnDelete(DeleteBehavior.SetNull);
            
            /*** QUOTE ***/

        }
    }
}
