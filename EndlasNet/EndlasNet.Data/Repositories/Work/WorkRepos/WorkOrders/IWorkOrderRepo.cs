using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkOrderRepo
    {
        public Task AddWorkOrder(WorkOrder workOrder);
        public Task DeleteWorkOrder(Guid? id);
        public Task<IEnumerable<WorkOrder>> GetAllWorkOrders();
        public Task<WorkOrder> GetWorkOrder(Guid? id);
        public Task<object> GetWorkOrderNoTracking(Guid? id);
        public Task<bool> WorkOrderExists(Guid id);
        public Task UpdateWorkOrder(WorkOrder workOrder);
        public Task<WorkOrder> FindWorkOrder(Guid? id);
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
