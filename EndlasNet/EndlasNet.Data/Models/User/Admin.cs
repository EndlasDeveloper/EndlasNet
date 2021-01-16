using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class Admin : User
    {
        public Admin() 
        {
            Privileges = 7;
        }
    }
}
