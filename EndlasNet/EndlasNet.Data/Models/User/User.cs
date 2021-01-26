using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EndlasNet.Data
{

    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EndlasEmail { get; set; }

        [StringLength(250, MinimumLength = 8)]
        public string AuthString { get; set; }

        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        public IEnumerable<Insert> Inserts { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}
