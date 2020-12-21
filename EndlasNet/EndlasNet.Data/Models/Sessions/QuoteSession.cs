using System;

namespace EndlasNet.Data
{
    /*
    * Class: QuoteSession
    * Description: Model object/entity describing the QuoteSession entity
    */
    public class QuoteSession
    {
        // PK
        public int QuoteSessionId { get; set; }
        // columns
        public string QuoteSessionName { get; set; }
        public DateTime QuoteSessionDate { get; set; }

        // FK reference
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}