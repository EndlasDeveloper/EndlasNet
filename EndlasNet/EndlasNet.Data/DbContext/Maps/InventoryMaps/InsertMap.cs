using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class InsertMap
    {

        public InsertMap(EntityTypeBuilder<Insert> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(i => i.InsertId);
            // not null
            entityBuilder.Property(i => i.PurchaseOrderNum).IsRequired();
            entityBuilder.Property(i => i.PurchaseOrderDate).IsRequired();
            entityBuilder.Property(i => i.PurchaseOrderPrice).IsRequired();
            entityBuilder.Property(i => i.Description).IsRequired();
            entityBuilder.Property(i => i.VendorPartNum).IsRequired();
            entityBuilder.Property(i => i.ToolTipRadius).IsRequired();
            entityBuilder.Property(i => i.InsertCount).IsRequired();

            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
        }
    }
}
