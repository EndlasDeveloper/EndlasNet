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
            entityBuilder.Property(v => v.VendorName).IsRequired();
            entityBuilder.Property(v => v.PointOfContact).IsRequired();
            entityBuilder.Property(v => v.VendorAddress).IsRequired();
            entityBuilder.Property(v => v.VendorPhone).IsRequired();

            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
