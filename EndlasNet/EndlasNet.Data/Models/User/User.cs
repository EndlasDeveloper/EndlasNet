using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string AuthString { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateAdded { get; set; }

        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        public IEnumerable<Insert> Inserts { get; set; }
        public IEnumerable<Vendor> Vendors { get; set; }
    }
}
