using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkOrderRepo : IWorkOrderRepo
    {
        private readonly EndlasNetDbContext _db;

        public WorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        
        public async Task AddWorkOrder(WorkOrder workOrder)
        {
            await _db.WorkOrders.AddAsync(workOrder);
            await _db.SaveChangesAsync();   
        }

        public async Task DeleteWorkOrder(Guid? id)
        {
            var workOrder = await _db.WorkOrders
                .FirstOrDefaultAsync(w => w.WorkId == id);
            _db.Remove(workOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetAllWorkOrders()
        {
            return await _db.WorkOrders
                .Include(w => w.Customer)
                .Include(w => w.Quote)
                .Include(w => w.User)
                .OrderByDescending(w => w.EndlasNumber)
                .ToListAsync();
        }

        public async Task<WorkOrder> GetWorkOrder(Guid? id)
        {
            return await _db.WorkOrders
                .Include(w => w.Customer)
                .Include(w => w.User)
                .Include(w => w.Quote)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<object> GetWorkOrderNoTracking(Guid? id)
        {
            return await _db.WorkOrders
                .AsNoTracking()
                .Include(w => w.Customer)
                .Include(w => w.User)
                .Include(w => w.Quote)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> WorkOrderExists(Guid id)
        {
            return await _db.WorkOrders
                .AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateWorkOrder(WorkOrder workOrder)
        {
            var entry = _db.Entry(workOrder);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();  
        }
        public async Task<WorkOrder> FindWorkOrder(Guid? id)
        {
            return await _db.WorkOrders.FindAsync(id);
        }

        public async Task<Work> GetWork(Guid? id)
        {
            return await _db.Work
                .Include(w => w.User)
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _db.Customers
                .OrderByDescending(c => c.CustomerName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            return await _db.Quotes
                .OrderByDescending(q => q.EndlasNumber)
                .ToListAsync();
        }

        public async Task<Quote> GetQuote(Guid id)
        {
            return await _db.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        }
        public async Task<IEnumerable<Work>> GetWorkWithEndlasNumber(string endlasNumber)
        {
            return await _db.Work
                .Where(w => w.EndlasNumber == endlasNumber)
                .ToListAsync();
        }

 

        public async Task<IEnumerable<Work>> FindDuplicateWork(Work work)
        {
            return await _db.Work
                .Where(w => w.WorkId != work.WorkId)
                .Where(w => w.EndlasNumber == work.EndlasNumber)
                .ToListAsync();
        }
        public async  Task<IEnumerable<Quote>> FindDuplicateQuote(Work work)
        {
            return await _db.Quotes
                .Where(q => q.EndlasNumber == work.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetQuotesWithEndlasNumber(string endlasNumber)
        {
            return await _db.Quotes
                 .Where(q => q.EndlasNumber == endlasNumber)
                 .ToListAsync();
        }
    }
}
