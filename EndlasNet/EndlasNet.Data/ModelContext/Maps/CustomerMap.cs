using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class CustomerMap
    {
        private Func<EntityTypeBuilder<Customer>> entity;

        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder)
        {
            Contract.Requires(entityBuilder != null);
            entityBuilder.HasKey(c => c.CustomerId);  // set PK
            // not null
            entityBuilder.Property(c => c.CompanyName).IsRequired();
            entityBuilder.Property(c => c.PointOfContact).IsRequired();
            entityBuilder.Property(c => c.Address).IsRequired();
            entityBuilder.Property(c => c.PhoneNumber).IsRequired();
        }

        public CustomerMap(Func<EntityTypeBuilder<Customer>> entity)
        {
            this.entity = entity;
        }

    }
}
