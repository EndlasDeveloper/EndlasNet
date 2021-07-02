using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPartForJobRepo
    {
        public Task<List<PartForJob>> GetAllPartsForJobs();
        public Task<PartForJob> GetPartForJobDetailsAsync(Guid? id);
        public Task<PartForJob> GetPartForJob(Guid? id);
        public Task<List<PartForJob>> GetExistingPartBatch(PartForJob partForJob);
        public Task AddPartForJobAsync(PartForJob partForJob);
        public Task DeletePartForJobConfirmedAsync(Guid id);
        public Task<bool> ConfirmPartForJobExistsAsync(Guid id);
        public Task<PartForJob> DeletePartForJobAsync(Guid? id);
        public Task UpdatePartForJobAsync(PartForJob partForJob);
        public Task<IEnumerable<PartForWork>> GetWorkItemBatch(Guid workItemId);
        public Task<IEnumerable<PartForJob>> GetBatch(string workId, string partInfoId);
        public Task<IEnumerable<Job>> GetJobsWithParts();
        public Task<IEnumerable<WorkOrder>> GetWorkOrdersWithParts();
        public Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfoWithoutJob();
        public Task<IEnumerable<Job>> GetJobsWithNoParts();
        public Task<IEnumerable<PartForWork>> GetAllPartsForWork();

        public Task<IEnumerable<PartForWorkOrder>> GetAllPartsForWorkOrders();
        public Task<IEnumerable<PartForWorkOrder>> GetPartForWorkOrders();

        public string GetWorkType(PartForWork partForWork);
        public Task<Customer> GetCustomer(Guid id);
        public Task<PartForWork> GetPartForWork(Guid? id);
        public Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo();

        public Task AddPartForWork(PartForWork partForWork);
        public Task UpdatePartForWork(PartForWork partForWork);

        public Task DeletePartForWork(PartForWork partForWork);
        public bool PartForWorkExists(Guid id);
        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<IEnumerable<Job>> GetAllJobs();
        public Task<Work> GetWork(Guid id);
        public Task<IEnumerable<PartForJob>> GetPartsForJobsWithPartInfo(Guid staticPartInfoId);



        // PART IMAGE
        public Task<IEnumerable<PartForWorkImg>> GetAllPartForWorkImgs();
        public Task<PartForWorkImg> GetPartForWorkImg(Guid id);
        public Task UpdatePartForWorkImg(PartForWorkImg partForWorkImg);
        public Task DeletePartForWorkImg(PartForWorkImg partForWorkImg);
        public Task AddPartForWorkImg(PartForWorkImg partForWorkImg);
        public Task<WorkItem> GetWorkItem(Guid? workItemId);
    }
}
