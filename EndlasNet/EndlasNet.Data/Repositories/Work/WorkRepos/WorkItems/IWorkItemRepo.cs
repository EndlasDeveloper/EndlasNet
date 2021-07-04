using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkItemRepo
    {
        public Task<WorkItem> GetWorkItem(Guid? workItemId);
        public Task<IEnumerable<WorkItem>> GetAllWorkItems();
        public Task AddWorkItem(WorkItem workItem);
        public Task UpdateWorkItem(WorkItem workItem);
        public Task DeleteWorkItem(Guid? id);
        public Task<Work> GetWork(Guid workId);

        public Task<IEnumerable<StaticPartInfo>> GetAllPartInfo();
        public Task<IEnumerable<WorkItem>> GetWorkItemsForWork(Guid workId);

        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<IEnumerable<PartForWork>> GetPartsForJobsWithPartInfo(Guid? staticPartInfoId);
        public Task<IEnumerable<PartForWork>> GetExistingPartBatch(Guid workId);
        public Task AddPartForJob(PartForJob partForJob);
        public Task<IEnumerable<PartForWork>> GetPartsForAWork(Guid workItemId);
        public Task DeletePartBatch(List<PartForWork> parts);
    }
}
