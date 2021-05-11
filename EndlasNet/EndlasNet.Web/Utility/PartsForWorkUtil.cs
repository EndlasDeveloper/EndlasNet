using EndlasNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Utility
{
    public class PartsForWorkUtil
    {
        public static async Task<List<PartForJob>> MinimizeJobPartList(List<PartForJob> parts, IPartForJobRepo repo)
        {
            List<PartForJob> minimizedPartList = new List<PartForJob>();
            var flag = false;
            foreach (PartForJob part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                for (int i = 0; i < minimizedPartList.Count; i++)
                {
                    if (minimizedPartList[i].WorkId.Equals(temp.Key))
                        if (minimizedPartList[i].StaticPartInfoId.Equals(temp.Value))
                        {
                            var list = await repo.GetBatch(part.WorkId.ToString(), part.StaticPartInfoId.ToString());
                            minimizedPartList[i].NumParts = list.Count();
                            flag = true;
                        }
                }
                if (!flag)
                {
                    minimizedPartList.Add(part);
                    flag = true;
                }
            }
            return minimizedPartList;
        }

        public static async Task<List<PartForWorkOrder>> MinimizeWorkOrderPartList(List<PartForWorkOrder> parts, IPartForWorkOrderRepo repo)
        {
            List<PartForWorkOrder> minimizedPartList = new List<PartForWorkOrder>();
            bool flag = false;

            foreach (PartForWorkOrder part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                for (int i = 0; i < minimizedPartList.Count; i++)
                {
                    if (minimizedPartList[i].WorkId.Equals(temp.Key))
                        if (minimizedPartList[i].StaticPartInfoId.Equals(temp.Value))
                        {
                            var list = await repo.GetBatch(part.WorkId.ToString(), part.StaticPartInfoId.ToString());
                            minimizedPartList[i].NumParts = list.Count();
                            flag = true;
                        }
                }
                if (!flag)
                {
                    minimizedPartList.Add(part);
                    flag = true;
                }
            }
            return minimizedPartList;
        }
    }
}
