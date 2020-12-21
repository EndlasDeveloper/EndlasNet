namespace EndlasNet.Data
{
    /*
    * Class: OptionalLaserService
    * Description: Model object/entity describing the OptionalLaserService entity
    */
    public class OptionalLaserService
    {
        // PK
        public int OptionalLaserServicesId { get; set; }
        // columns
        public double HeatTreatedBlankWt { get; set; }
        public double HeatTreatedPricePerLb { get; set; }
        public double MinHeatTreatmentPrice { get; set; }

    }
}
