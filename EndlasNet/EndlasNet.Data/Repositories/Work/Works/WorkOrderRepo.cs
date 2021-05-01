﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkOrderRepo : IWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public WorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(WorkOrder workOrder)
        {
            await _db.WorkOrders.AddAsync(workOrder);
            await _db.SaveChangesAsync();   
        }

        public async Task DeleteRow(Guid? id)
        {
            var workOrder = await _db.WorkOrders
                .FirstOrDefaultAsync(w => w.WorkId == id);
            _db.Remove(workOrder);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkOrder>> GetAllRows()
        {
            return await _db.WorkOrders
                .Include(w => w.Customer)
                .Include(w => w.Quote)
                .Include(w => w.User)
                .OrderByDescending(w => w.EndlasNumber)
                .ToListAsync();
        }

        public async Task<WorkOrder> GetRow(Guid? id)
        {
            return await _db.WorkOrders
                .Include(w => w.Customer)
                .Include(w => w.User)
                .Include(w => w.Quote)
                .FirstOrDefaultAsync(w => w.WorkId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
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

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.WorkOrders
                .AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateRow(WorkOrder workOrder)
        {
            var entry = _db.Entry(workOrder);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();  
        }
        public async Task<WorkOrder> FindRow(Guid? id)
        {
            return await _db.WorkOrders.FindAsync(id);
        }
    }
}
