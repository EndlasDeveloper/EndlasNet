using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndlasNet.Data
{
    internal class PartForWorkOrderMap
    {
        private EntityTypeBuilder<PartForWorkOrder> entityTypeBuilder;

        public PartForWorkOrderMap(EntityTypeBuilder<PartForWorkOrder> entityTypeBuilder)
        {
            this.entityTypeBuilder = entityTypeBuilder;
        }
    }
}