﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkOrderRepo : IMachiningToolForWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public MachiningToolForWorkOrderRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var machiningToolForWorkOrder = (MachiningToolForWorkOrder)obj;
                switch (machiningToolForWorkOrder.MachiningType)
                {
                    case MachiningTypes.Blanking:
                        await _db.MachiningToolsForWorkOrdersBlanking.AddAsync(machiningToolForWorkOrder);
                        break;
                    case MachiningTypes.Finishing:
                        await _db.MachiningToolsForWorkOrdersFinishing.AddAsync(machiningToolForWorkOrder);
                        break;
                    default:
                        break;
                }
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningToolForJob = await _db.MachiningToolsForWorkOrders.FindAsync(id);
            _db.MachiningToolsForWorkOrders.Remove(machiningToolForJob);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.MachiningToolsForWorkOrders
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .OrderByDescending(m => m.DateUsed)
                .ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.MachiningToolsForWorkOrders
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.MachiningToolsForWorkOrders
                .AsNoTracking()
                .Include(m => m.MachiningTool)
                .Include(m => m.Work)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.MachiningToolsForWorkOrders
                          .AnyAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var entry = _db.Entry((MachiningToolForJob)obj);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
