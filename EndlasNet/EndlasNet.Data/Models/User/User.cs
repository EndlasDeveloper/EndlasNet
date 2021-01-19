using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /* Privileges: the 3 least significant bits represent:
         * R: Read, W: Write, D: Delete, D*: Delete only entries made by self
                       __  __  __ lsd
                       |   |   |
                   R/W/D R/W/D* R
         */
        // default privelege to read/write/del*
        public IEnumerable<InsertToJob> InsertToJobs { get; set; }
        protected User() { }
    }
}
