namespace EndlasNet.Data
{
    /*
    * Class: MachineQuoteSession
    * Description: Model object/entity describing the MachineQuoteSession entity
    */
    public class MachineQuoteSession
    {
        // PK
        public int MachineQuoteSessionId { get; set; }
        

        // FK reference
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}