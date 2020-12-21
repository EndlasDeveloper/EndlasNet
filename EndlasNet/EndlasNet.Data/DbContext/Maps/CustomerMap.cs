using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    /*
     * Class: CustomerMap
     * Description: Map object to describe the columns in Customer entity
     */
    public class CustomerMap
    {
        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder)
        {
            // make .NET happpy
            Contract.Requires(entityBuilder != null);
            // set PK
            entityBuilder.HasKey(c => c.CustomerId);  
            // not null
            entityBuilder.Property(c => c.CompanyName).IsRequired();
            entityBuilder.Property(c => c.PointOfContact).IsRequired();
            entityBuilder.Property(c => c.Address).IsRequired();
            entityBuilder.Property(c => c.PhoneNumber).IsRequired();
        }
    }
}