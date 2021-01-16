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
            entityBuilder.Property(e => e.Privileges).IsRequired();
        }
    }
}
