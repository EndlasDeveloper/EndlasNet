using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkRepo : IWorkRepo
    {
        protected EndlasNetDbContext _db;
        public WorkRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Work>> GetAllWork()
        {
            return await _db.Work
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .ToListAsync();
        }

        public async Task<Work> GetWork(Guid? id)
        {
            return await _db.Work
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task DeleteWork(Guid? id)
        {
            var work = await _db.Work
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(work);
            await _db.SaveChangesAsync();
        }

        public string GetWorkType(Work work)
        {
            return _db.Entry(work).Property("Discriminator").CurrentValue.ToString();
        }

        /******************************************************************************************/


        public async Task<IEnumerable<Quote>> GetAllQuotesWithoutJob()
        {
            var jobs = await _db.Jobs
                .Include(j => j.Quote)
                .ToListAsync();
            var allQuotes = await _db.Quotes.ToListAsync();
            List<Quote> jobQuotes = new List<Quote>();
            foreach (Job job in jobs)
            {
                jobQuotes.Insert(0, job.Quote);
            }
            List<Quote> returnQuotes = new List<Quote>();
            foreach (Quote quote in allQuotes)
            {
                returnQuotes.Insert(0, quote);
            }
            foreach (Quote aQuote in allQuotes)
            {
                foreach (Quote jQuote in jobQuotes)
                {
                    if (aQuote.QuoteId == jQuote.QuoteId)
                    {
                        returnQuotes.Remove(aQuote);
                    }
                }
            }
            return returnQuotes.OrderByDescending(q => q.EndlasNumber);
        }

        public async Task<Quote> GetQuote(Guid id)
        {
            return await _db.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        }

        public async Task<IEnumerable<Quote>> GetQuotesWithEndlasNumber(string endlasNumber)
        {
            return await _db.Quotes
                 .Where(q => q.EndlasNumber == endlasNumber)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> FindDuplicateQuote(Work work)
        {
            return await _db.Quotes
                .Where(q => q.EndlasNumber == work.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            return await _db.Quotes.ToListAsync();
        }

        public async Task<StaticPartInfo> GetStaticPartInfo(Guid id)
        {
            return await _db.StaticPartInfo.FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllPartInfo()
        {
            return await _db.StaticPartInfo.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _db.Customers
                .OrderByDescending(c => c.CustomerName)
                .ToListAsync();
        }

        public async Task<Customer> GetCustomer(Guid? id)
        {
            return await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public Task<IEnumerable<Work>> GetWorkWithEndlasNumber(string endlasNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Work>> FindDuplicateWork(Work work)
        {
            throw new NotImplementedException();
        }

        public async Task AddJob(Job job)
        {
            _db.Add(job);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteJob(Guid? id)
        {
            try
            {
                var job = await _db.Jobs.FirstOrDefaultAsync(j => j.WorkId == id);
                var entry = _db.Entry(job);
                entry.State = EntityState.Deleted;
                _db.Jobs.Remove(job);
                await _db.SaveChangesAsync();
            }
            catch (Exception) { }
        }

        public async Task<IEnumerable<Job>> GetAllJobs()
        {
           return await _db.Jobs.Include(j => j.Customer)
                .Include(j => j.Quote)
                .Include(j => j.WorkItems)
                .Include(j => j.MachiningToolsForWork)
                .ToListAsync();
        }

        public async Task<Job> GetJob(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .Include(j => j.Quote)
                .Include(j => j.Customer)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task<Job> GetJobNoTracking(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.Quote)
                .Include(j => j.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public Task<bool> JobExists(Guid id)
        {
            return _db.Jobs.AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateJob(Job job)
        {
            var entry = _db.Entry(job);
            entry.State = EntityState.Modified;
            _db.Jobs.Update(job);
            await _db.SaveChangesAsync();
        }

        public async Task<Job> FindJob(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .Include(j => j.Quote)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task AddWorkOrder(WorkOrder workOrder)
        {
            _db.WorkOrders.Add(workOrder);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWorkOrder(Guid? id)
        {
            var workOrder = await _db.WorkOrders.FirstOrDefaultAsync(w => w.WorkId == id);
            var entry = _db.Entry(workOrder);
            entry.State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetAllWorkOrders()
        {
            return await _db.WorkOrders
                .Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .ToListAsync();
        }

        public async Task<WorkOrder> GetWorkOrder(Guid? id)
        {
            return await _db.WorkOrders
                .Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<WorkOrder> GetWorkOrderNoTracking(Guid? id)
        {
            return await _db.WorkOrders.AsNoTracking()
                           .Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                           .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<bool> WorkOrderExists(Guid id)
        {
            return await _db.WorkOrders.AnyAsync(w => w.WorkId == id);
        }

        public async Task UpdateWorkOrder(WorkOrder workOrder)
        {
            var entry = _db.Entry(workOrder);
            entry.State = EntityState.Modified;
            _db.WorkOrders.Update(workOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<WorkOrder> FindWorkOrder(Guid? id)
        {
            return await _db.WorkOrders
                .Include(w => w.WorkItems).ThenInclude(w => w.StaticPartInfo)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task AddWorkItem(WorkItem workItem)
        {
            _db.WorkItems.Add(workItem);

            await _db.SaveChangesAsync();
        }

        public async Task<WorkItem> GetWorkItem(Guid? workItemId)
        {
            return await _db.WorkItems
                .Include(w => w.StaticPartInfoId)
                .Include(w => w.PartsForWork)
                .FirstOrDefaultAsync(w => w.WorkItemId == workItemId);
        }

        public async Task<IEnumerable<WorkItem>> GetAllWorkItems()
        {
            return await _db.WorkItems
                .Include(w => w.Work)
                .Include(w => w.PartsForWork)
                .Include(w => w.StaticPartInfo)
                .ToListAsync();
        }

        public async Task UpdateWorkItem(WorkItem workItem)
        {
            var entry = _db.Entry(workItem);
            entry.State = EntityState.Modified;
            _db.WorkItems.Update(workItem);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWorkItem(Guid? id)
        {
            try
            {
                var workItem = await _db.WorkItems.FirstOrDefaultAsync(w => w.WorkItemId == id);
                var entry = _db.Entry(workItem);
                entry.State = EntityState.Deleted;
                _db.WorkItems.Remove(workItem);
                await _db.SaveChangesAsync();
            }
            catch (Exception) { }
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItemsForWork(Guid workId)
        {
            return await _db.WorkItems
                .Include(w => w.Work)
                .Include(w => w.PartsForWork)
                .Include(w => w.StaticPartInfo)
                .Where(w => w.WorkId == workId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForJobsWithPartInfo(Guid? staticPartInfoId)
        {
            return await _db.PartsForWork
                .Include(p => p.WorkItem).ThenInclude(w => w.StaticPartInfo)
                .ToListAsync();
        }

        public Task<IEnumerable<PartForWork>> GetExistingPartBatch(Guid workId)
        {
            throw new NotImplementedException();
        }

        public async Task AddPartForJob(PartForJob partForJob)
        {
            var entry = _db.Entry(partForJob);
            entry.State = EntityState.Added;
            _db.PartsForJobs.Add(partForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<PartForWork>> GetPartsForAWork(Guid workItemId)
        {
            return await _db.PartsForWork
                .Include(p => p.WorkItem).ThenInclude(w => w.Work)
                .Where(p => p.WorkItemId == workItemId)
                .ToListAsync();
        }

        public async Task DeletePartForWork(Guid? id)
        {
            var part = await _db.PartsForWork.FirstOrDefaultAsync(p => p.PartForWorkId == id);
            var entry = _db.Entry(part);
            entry.State = EntityState.Deleted;
            _db.Remove(part);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePartBatch(List<PartForWork> parts)
        {
            for(int i = 0; i < parts.Count(); i++)
            {
                try
                {
                    await DeletePartForWork(parts[i].PartForWorkId);
                }
                catch (Exception) { continue; }
            }
        }
    }
}
