using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Utility
{
    public class PowderBottleUtil
    {
        /*
       * Name: GetCostPerPound
       * Description: returns the powder cost (in dollars) per pound of powder.
       */
        public static float GetCostPerPound(float lineItemCostPerBottle, float feePerBottle, float numberOfPounds)
        {
            return (lineItemCostPerBottle + feePerBottle) / numberOfPounds;
        }

        /*
         * Name: GetFeePerBottle
         * Description: returns a normalized fee based on the shipping, tax, and number of bottles
         */
        public static float? GetFeePerBottle(float shippingCost, float taxCost, int numberOfBottles)
        {
            return (shippingCost + taxCost) / numberOfBottles;
        }
    }
}
