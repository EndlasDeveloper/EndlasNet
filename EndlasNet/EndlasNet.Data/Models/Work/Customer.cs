using System;
using System.Collections.Generic;

namespace EndlasNet.Data
{
    /*
    * Class: Customer
    * Description: Model object/entity describing the Customer entity
    */
    public class Customer
    {
        // PK
        public Guid CustomerId { get; set; }
        // columns
        public string CustomerName { get; set; }
        public string PointOfContact { get; set; } // Point Of Contact
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        // customer has 0 to many jobs
        public IEnumerable<Job> Jobs { get; set; }
    }
}
