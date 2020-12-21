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
        public string PointOfContact { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
