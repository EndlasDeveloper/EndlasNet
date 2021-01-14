using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }

    }
}
