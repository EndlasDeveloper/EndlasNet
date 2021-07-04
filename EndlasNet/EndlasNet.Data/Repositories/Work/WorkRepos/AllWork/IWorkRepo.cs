using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkRepo
    {
        // QUOTE
        public Task<IEnumerable<Quote>> GetAllQuotesWithoutJob();
        public Task<Quote> GetQuote(Guid id);
        public Task<IEnumerable<Quote>> GetQuotesWithEndlasNumber(string endlasNumber);
        public Task<IEnumerable<Quote>> FindDuplicateQuote(Work work);
        public Task<IEnumerable<Quote>> GetAllQuotes();

        // STATIC PART INFORMATION
        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<IEnumerable<StaticPartInfo>> GetAllPartInfo();

        // CUSTOMER
        public Task<IEnumerable<Customer>> GetAllCustomers();
        public Task<Customer> GetCustomer(Guid? id);


        //  WORK
        public Task<IEnumerable<Work>> GetAllWork();
        public Task<Work> GetWork(Guid? id);
        public Task DeleteWork(Guid? id);
        public string GetWorkType(Work work);
        public Task<IEnumerable<Work>> GetWorkWithEndlasNumber(string endlasNumber);
        public Task<IEnumerable<Work>> FindDuplicateWork(Work work);


        // JOB
        public Task AddJob(Job job);
        public Task DeleteJob(Guid? id);
        public Task<IEnumerable<Job>> GetAllJobs();
        public Task<Job> GetJob(Guid? id);
        public Task<Job> GetJobNoTracking(Guid? id);
        public Task<bool> JobExists(Guid id);
        public Task UpdateJob(Job job);
        public Task<Job> FindJob(Guid? id);

        // WORK ORDER
        public Task AddWorkOrder(WorkOrder workOrder);
        public Task DeleteWorkOrder(Guid? id);
        public Task<IEnumerable<WorkOrder>> GetAllWorkOrders();
        public Task<WorkOrder> GetWorkOrder(Guid? id);
        public Task<WorkOrder> GetWorkOrderNoTracking(Guid? id);
        public Task<bool> WorkOrderExists(Guid id);
        public Task UpdateWorkOrder(WorkOrder workOrder);
        public Task<WorkOrder> FindWorkOrder(Guid? id);


        // WORK ITEM
        public Task AddWorkItem(WorkItem workItem);
        public Task<WorkItem> GetWorkItem(Guid? workItemId);
        public Task<IEnumerable<WorkItem>> GetAllWorkItems();
        public Task UpdateWorkItem(WorkItem workItem);
        public Task DeleteWorkItem(Guid? id);
        public Task<IEnumerable<WorkItem>> GetWorkItemsForWork(Guid workId);


        // PART FOR WORK
        public Task<IEnumerable<PartForWork>> GetPartsForJobsWithPartInfo(Guid? staticPartInfoId);
        public Task<IEnumerable<PartForWork>> GetExistingPartBatch(Guid workId);
        public Task AddPartForJob(PartForJob partForJob);
        public Task<IEnumerable<PartForWork>> GetPartsForAWork(Guid workItemId);
        public Task DeletePartBatch(List<PartForWork> parts);




    }
}
