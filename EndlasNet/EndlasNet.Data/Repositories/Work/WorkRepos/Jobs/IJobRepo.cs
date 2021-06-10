using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IJobRepo
    {
        public Task AddRow(Job job);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<Job>> GetAllRows();
        public Task<Job> GetRow(Guid? id);
        public Task<Job> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(Job job);
        public Task<Job> FindRow(Guid? id);
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
