﻿    namespace EndlasNet.Data
{
    /*
    * Class: IntermediateParam
    * Description: Model object/entity describing the IntermediateParam entity
    */
    public class IntermediateParam
    {
        // PK
        public int IntermediateParamId { get; set; }
        // columns
        public double SurfaceVelocity { get; set; }
        public double StepMm { get; set; }
        public double StepIn { get; set; }
        public double AssumedAvgPassLenIn { get; set; }
        public double PseudoWidthIn { get; set; }
        public int PseudoNumPasses { get; set; }
        public double TimePerBeadSec { get; set; }
        public double TimeBetweenBeadsMin { get; set; }
        public double TimePerLayerMin { get; set; }
        public double CladAddRateSqInPerMin { get; set; }
        public double ApproxVolPerLayerCubicCm { get; set; }

        // FK reference
        public int LaserQuoteSessionId { get; set; }
        public LaserQuoteSession LaserQuoteSession { get; set; }
    }
}