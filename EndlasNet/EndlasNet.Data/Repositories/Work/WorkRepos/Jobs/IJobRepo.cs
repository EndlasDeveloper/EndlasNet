using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IJobRepo
    {
        public Task AddJob(Job job);
        public Task DeleteJob(Guid? id);
        public Task<IEnumerable<Job>> GetAllJobs();
        public Task<Job> GetJob(Guid? id);
        public Task<Job> GetJobNoTracking(Guid? id);
        public Task<bool> JobExists(Guid id);
        public Task UpdateJob(Job job);
        public Task<Job> FindJob(Guid? id);
        public Task<IEnumerable<Work>> GetAllWork();
        public Task<Work> GetWork(Guid? id);
        public Task DeleteWork(Guid? id);
        public string GetWorkType(Work work);
        public Task<Quote> GetQuote(Guid id);

        public Task<IEnumerable<Customer>> GetAllCustomers();
        public Task<IEnumerable<Quote>> GetAllQuotesWithoutJob();
        public Task AddWorkItem(WorkItem workItem);
    }
}
