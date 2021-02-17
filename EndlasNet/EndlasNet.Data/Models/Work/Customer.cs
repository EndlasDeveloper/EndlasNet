using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        [Display(Name = "Point of contact")]
        public string PointOfContact { get; set; }
        [Display(Name = "Customer address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer phone")]
        public string CustomerPhone { get; set; }
        // customer has 0 to many jobs
        public IEnumerable<Work> Work { get; set; }
    }
}
