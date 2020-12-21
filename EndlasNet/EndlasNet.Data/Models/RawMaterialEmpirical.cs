namespace EndlasNet.Data
{
    /*
    * Class: RawMaterialEmpirical
    * Description: Model object/entity describing the RawMaterialEmpirical entity
    */
    public class RawMaterialEmpirical
    {
        public int RawMaterialEmpiricalId { get; set; }
        public double FlowRateSlope { get; set; }
        public double FlowRateYIntercept { get; set; }
    }
}
