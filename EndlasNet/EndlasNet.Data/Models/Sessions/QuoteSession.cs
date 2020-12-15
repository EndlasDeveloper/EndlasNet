using System;

namespace EndlasNet.Data
{
    public class QuoteSession
    {
        // PK
        public int QuoteSessionId { get; set; }
        // columns
        public string QuoteSessionName { get; set; }
        public DateTime QuoteSessionDate { get; set; }

        // FK reference
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}