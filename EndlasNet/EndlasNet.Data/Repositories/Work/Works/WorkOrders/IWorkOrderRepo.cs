using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkOrderRepo
    {
        public Task AddRow(WorkOrder workOrder);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<WorkOrder>> GetAllRows();
        public Task<WorkOrder> GetRow(Guid? id);
        public Task<object> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(WorkOrder workOrder);
        public Task<WorkOrder> FindRow(Guid? id);
        public Task<Quote> GetQuote(Guid id);
        public Task<Work> GetWork(Guid? id);
        public Task<IEnumerable<Work>> GetWorkWithEndlasNumber(string endlasNumber);
        public Task<IEnumerable<Quote>> GetQuotesWithEndlasNumber(string endlasNumber);
        public Task<IEnumerable<Work>> FindDuplicateWork(Work work);
        public Task<IEnumerable<Quote>> FindDuplicateQuote(Work work);
        public Task<IEnumerable<Customer>> GetAllCustomers();
        public Task<IEnumerable<Quote>> GetAllQuotes();
    }
}
