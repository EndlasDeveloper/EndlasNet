using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public static class PartForWorkUtil
    {
        public static async Task<List<PartForJob>> MinimizeJobPartList(List<PartForJob> parts, PartForJobRepo repo)
        {
            List<PartForJob> minimizedPartList = new List<PartForJob>();
            foreach (PartForJob part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                bool flag = false;
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
                    minimizedPartList.Add(part);
            }
            return minimizedPartList;
        }

        public static async Task<List<PartForWorkOrder>> MinimizeWorkOrderPartList(List<PartForWorkOrder> parts, PartForWorkOrderRepo repo)
        {
            List<PartForWorkOrder> minimizedPartList = new List<PartForWorkOrder>();
            foreach (PartForWorkOrder part in parts)
            {
                KeyValuePair<Guid, Guid> temp = new KeyValuePair<Guid, Guid>(part.WorkId, part.StaticPartInfoId);
                bool flag = false;
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
                    minimizedPartList.Add(part);
            }
            return minimizedPartList;
        }
    }
}
