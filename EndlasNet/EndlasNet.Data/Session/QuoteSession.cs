using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class QuoteSession
    {
        public int QuoteSessionId { get; set; }
        public string QuoteSessionName { get; set; }
        public DateTime QuoteSessionDate { get; set; }
        
        // FK reference
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
