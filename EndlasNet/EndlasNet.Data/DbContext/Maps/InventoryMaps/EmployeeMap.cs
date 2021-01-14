using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class EmployeeMap
    {

        public EmployeeMap(EntityTypeBuilder<Employee> entityBuilder)
        {
            // set PK
            entityBuilder.HasKey(e => e.EmployeeId);           
        }
    }
}
