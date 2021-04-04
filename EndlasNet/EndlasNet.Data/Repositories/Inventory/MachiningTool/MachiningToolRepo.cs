﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolRepo : IMachiningToolRepo
    {
        private readonly EndlasNetDbContext _db;
        public MachiningToolRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task AddRow(object obj)
        {
            try
            {
                var machiningTool = (MachiningTool)obj;
                var entry = _db.MachiningTools.AddAsync(machiningTool);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningTool = await _db.MachiningTools.FindAsync(id);
            _db.MachiningTools.Remove(machiningTool);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.MachiningTools
                .Include(m => m.Vendor)
                .Include(m => m.User)
                .OrderBy(m => m.ToolType)
                .ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.MachiningTools
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.MachiningTools
                .AsNoTracking()
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.MachiningTools
                .AnyAsync(m => m.MachiningToolId == id);
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var machiningTool = (MachiningTool)obj;
                var entry = _db.Entry(machiningTool);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task<MachiningTool> FindRow(Guid? id)
        {
            return await _db.MachiningTools.FindAsync(id);
        }

        public async Task<List<MachiningTool>> GetAvailableTools()
        {
            return await _db.MachiningTools.Where(m => m.ToolCount > 0).ToListAsync();
        }
    }
}