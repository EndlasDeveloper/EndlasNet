using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
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
            // set PK
            entityBuilder.HasKey(c => c.CustomerId);  
            // not null
            entityBuilder.Property(c => c.CustomerName).IsRequired();
            entityBuilder.Property(c => c.PointOfContact).IsRequired();
            entityBuilder.Property(c => c.CustomerAddress).IsRequired();
            entityBuilder.Property(c => c.CustomerPhone).IsRequired();
        }
    }
}