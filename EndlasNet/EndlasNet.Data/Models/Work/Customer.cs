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
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Point of contact")]
        public string PointOfContact { get; set; }

        [Required]
        [Display(Name = "Customer address")]
        public string CustomerAddress { get; set; }

        [Required]
        [Display(Name = "Customer phone")]
        public string CustomerPhone { get; set; }
        // customer has 0 to many jobs
        public IEnumerable<Work> Work { get; set; }
    }
}
