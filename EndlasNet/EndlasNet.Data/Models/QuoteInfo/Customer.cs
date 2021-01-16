namespace EndlasNet.Data
{
    /*
    * Class: Customer
    * Description: Model object/entity describing the Customer entity
    */
    public class Customer
    {
        // PK
        public int CustomerId { get; set; }
        // columns
        public string CompanyName { get; set; }
        public string POC { get; set; } // Point Of Contact
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
