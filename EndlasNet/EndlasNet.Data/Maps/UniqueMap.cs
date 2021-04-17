using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class UniqueMap
    {
        public UniqueMap(ModelBuilder modelBuilder)
        {
            // unique
            modelBuilder.Entity<User>().HasIndex(u => u.EndlasEmail).IsUnique();
            modelBuilder.Entity<Work>().HasIndex(w => w.EndlasNumber).IsUnique();
        }
    }
}
