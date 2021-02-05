using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class MachiningToolMap
    {
        public MachiningToolMap(EntityTypeBuilder<MachiningTool> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(m => m.MachiningToolId);
            // not null
            entityBuilder.Property(m => m.ToolType).IsRequired();
            entityBuilder.Property(m => m.ToolDiameter).IsRequired();
            entityBuilder.Property(m => m.VendorDescription).IsRequired();
            entityBuilder.Property(m => m.ToolCount).IsRequired();

            entityBuilder.Property(m => m.PurchaseOrderNum).IsRequired();
            entityBuilder.Property(m => m.PurchaseOrderDate).IsRequired();
            entityBuilder.Property(m => m.PurchaseOrderPrice).IsRequired();

            // shadow properties
            entityBuilder.Property<DateTime>("CreatedDate");
            entityBuilder.Property<DateTime>("UpdatedDate");
            // extra to Insert
        }
    }
}
