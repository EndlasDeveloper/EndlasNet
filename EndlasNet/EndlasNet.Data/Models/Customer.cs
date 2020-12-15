namespace EndlasNet.Data
{
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
