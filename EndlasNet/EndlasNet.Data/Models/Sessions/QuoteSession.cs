using System;

namespace EndlasNet.Data
{
    public class QuoteSession
    {
        public int QuoteSessionId { get; set; }
        public string QuoteSessionName { get; set; }
        public DateTime QuoteSessionDate { get; set; }

        // FK reference : QuoteSession has 0:1 Customers
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}