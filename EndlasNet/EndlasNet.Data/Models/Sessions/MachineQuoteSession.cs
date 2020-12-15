namespace EndlasNet.Data
{
    public class MachineQuoteSession
    {
        // PK
        public int MachineQuoteSessionId { get; set; }
        

        // FK reference
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}