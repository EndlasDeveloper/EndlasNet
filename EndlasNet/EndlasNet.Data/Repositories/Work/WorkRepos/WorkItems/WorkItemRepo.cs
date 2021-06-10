﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkItemRepo : IWorkItemRepo
    {
        private readonly EndlasNetDbContext _db;

        public WorkItemRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(WorkItem workItem)
        {
            await _db.WorkItems.AddAsync(workItem);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var workItem = await _db.WorkItems.FirstOrDefaultAsync(w => w.WorkItemId == id);
            _db.WorkItems.Remove(workItem);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkItem>> GetAllRows()
        {
            return await _db.WorkItems.ToListAsync();
        }

        public async Task<WorkItem> GetRow(Guid? workItemId)
        {
            return await _db.WorkItems
                .Include(l => l.Work)
                .Include(l => l.PartsForWork)
                .FirstOrDefaultAsync(l => l.WorkItemId == workItemId);
        }

        public async Task UpdateRow(WorkItem workItem)
        {
            var entry = _db.Entry(workItem);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}