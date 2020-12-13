using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class MachineSession
    {
        public int MachineSessionId { get; set; }

        // FK reference
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }

    }
}
