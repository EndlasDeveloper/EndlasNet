namespace EndlasNet.Data
{
    public class MachineQuoteSession
    {
        public int MachineQuoteSessionId { get; set; }
        

        // FK reference : MachineQuoteSession
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}