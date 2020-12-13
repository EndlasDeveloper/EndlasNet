using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class LaserQuoteSession
    {
        public int LaserQuoteSessionId { get; set; }


        // FK reference
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }

    }
}
