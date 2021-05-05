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
        public Task<IEnumerable<PartForJob>> GetBatch(string workId, string partInfoId);

        public Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo();
        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<IEnumerable<Job>> GetAllJobs();
        public Task<Work> GetWork(Guid id);
        public Task<IEnumerable<PartForJob>> GetPartsForJobsWithPartInfo(Guid staticPartInfoId);

    }
}
