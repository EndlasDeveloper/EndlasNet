using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class VendorMap
    {

        public VendorMap(EntityTypeBuilder<Vendor> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(v => v.VendorId);
            // not null
            entityBuilder.Property(v => v.Name).IsRequired();
            entityBuilder.Property(v => v.POC).IsRequired();
            entityBuilder.Property(v => v.Address).IsRequired();
            entityBuilder.Property(v => v.Phone).IsRequired();          
        }
    }
}
