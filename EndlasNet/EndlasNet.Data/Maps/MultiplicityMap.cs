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
            modelBuilder.Entity<Customer>().HasMany(c => c.StaticPartInfos).WithOne(c => c.Customer).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.LineItems).WithOne(l => l.StaticPowderInfo).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<StaticPowderInfo>().HasMany(s => s.Powders).WithOne(l => l.StaticPowderInfo).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<MachiningTool>().HasOne(m => m.User).WithMany(u => u.MachiningTools);
            modelBuilder.Entity<PowderBottle>().HasMany(p => p.PowderForParts).WithOne(p => p.PowderBottle).OnDelete(DeleteBehavior.SetNull);

            /*** USER ***/
            modelBuilder.Entity<User>().HasMany(u => u.MachiningTools).WithOne(i => i.User).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasMany(u => u.PowderBottles).WithOne(p => p.User).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<User>().HasMany(u => u.Work).WithOne(j => j.User).OnDelete(DeleteBehavior.SetNull);

            /*** WORK ***/
            modelBuilder.Entity<Work>().HasMany(j => j.PartsForWork).WithOne(p => p.Work);
            modelBuilder.Entity<PartForWork>().HasMany(p => p.PowdersUsed).WithOne(p => p.PartForWork);

            /*** ACTION ***/
            modelBuilder.Entity<Work>().HasMany(j => j.ToolsForJob).WithOne(m => m.Work).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Work>().HasMany(j => j.ToolsForWorkOrder).WithOne(m => m.Work).OnDelete(DeleteBehavior.SetNull);

            /*** QUOTE ***/

        }
    }
}
