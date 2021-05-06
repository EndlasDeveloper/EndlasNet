using Microsoft.EntityFrameworkCore;
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
        public async Task AddRow(MachiningTool machiningTool)
        {
            await _db.MachiningTools.AddAsync(machiningTool);
            await _db.SaveChangesAsync();   
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningTool = await _db.MachiningTools.FindAsync(id);
            _db.MachiningTools.Remove(machiningTool);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<MachiningTool>> GetAllRows()
        {
            return await _db.MachiningTools
                .Include(m => m.Vendor)
                .Include(m => m.User)
                .OrderByDescending(m => m.ToolType)
                .ToListAsync();
        }

        public async Task<MachiningTool> GetRow(Guid? id)
        {
            return await _db.MachiningTools
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
        }

        public async Task<MachiningTool> GetRowNoTracking(Guid? id)
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

        public async Task UpdateRow(MachiningTool machiningTool)
        {
            var entry = _db.Entry(machiningTool);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<MachiningTool> FindRow(Guid? id)
        {
            return await _db.MachiningTools.FindAsync(id);
        }

        public async Task<List<MachiningTool>> GetAvailableTools()
        {
            return await _db.MachiningTools
                .Where(m => m.ToolCount > 0)
                .OrderByDescending(m => m.ToolType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vendor>> GetAllVendors()
        {
            return await _db.Vendors
                .OrderByDescending(v => v.VendorName)
                .ToListAsync();
        }
    }
}
