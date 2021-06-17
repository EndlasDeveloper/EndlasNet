using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public static class PartForWorkUtil
    {
        public static async Task<List<PartForJob>> MinimizeJobPartList(List<PartForJob> parts, IPartForJobRepo repo)
        {
            List<PartForJob> minimizedPartList = new List<PartForJob>();
            var flag = false;
            foreach (PartForJob part in parts)
            {
                
            }
            return minimizedPartList;
        }

        public static async Task<List<PartForWorkOrder>> MinimizeWorkOrderPartList(List<PartForWorkOrder> parts, IPartForWorkOrderRepo repo)
        {
            List<PartForWorkOrder> minimizedPartList = new List<PartForWorkOrder>();
            bool flag = false;

            foreach (PartForWorkOrder part in parts)
            {
               
            }
            return minimizedPartList;
        }
    }
}
