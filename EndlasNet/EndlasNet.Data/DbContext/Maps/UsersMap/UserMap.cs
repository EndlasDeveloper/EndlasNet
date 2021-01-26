using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class UserMap
    {

        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(e => e.UserId);
            // not null
            entityBuilder.Property(e => e.FirstName).IsRequired();
            entityBuilder.Property(e => e.LastName).IsRequired();
            entityBuilder.Property(u => u.AuthString).IsRequired();
            entityBuilder.Property(u => u.EndlasEmail).IsRequired();
            // unique
            entityBuilder.HasIndex(u => u.EndlasEmail).IsUnique();
            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
