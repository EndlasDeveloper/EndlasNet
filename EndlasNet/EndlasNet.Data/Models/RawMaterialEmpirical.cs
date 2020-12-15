using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class RawMaterialEmpirical
    {
        public int RawMaterialEmpiricalId { get; set; }
        public double FlowRateSlope { get; set; }
        public double FlowRateYIntercept { get; set; }
    }
}
