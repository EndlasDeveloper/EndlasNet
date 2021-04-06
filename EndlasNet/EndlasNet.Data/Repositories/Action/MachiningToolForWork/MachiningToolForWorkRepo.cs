using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class MachiningToolForWorkRepo : IMachiningToolForWorkRepo
    {
        private readonly EndlasNetDbContext _db;
        private readonly MachiningToolForJobRepo _machiningToolForJobRepo;
        private readonly MachiningToolForWorkOrderRepo _machiningToolForWorkOrderRepo;

        public MachiningToolForWorkRepo(EndlasNetDbContext db)
        {
            _db = db;
            _machiningToolForJobRepo = new MachiningToolForJobRepo(db);
            _machiningToolForWorkOrderRepo = new MachiningToolForWorkOrderRepo(db);
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var machiningToolForWork = (IMachiningToolForWork)obj;
                var work = await _db.Work
                    .FirstOrDefaultAsync(w => w.WorkId == machiningToolForWork.WorkId);
                var workType = _db.Entry(work).Property("Discriminator").CurrentValue;
                if((string)workType == nameof(Job))
                {
                    await _machiningToolForJobRepo.AddRow(machiningToolForWork);
                }
                else if((string)(workType) == nameof(WorkOrder))
                {
                    await _machiningToolForWorkOrderRepo.AddRow(machiningToolForWork);
                }
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var machiningToolForWork = await _db.MachiningToolsForWork.FindAsync(id);
            _db.MachiningToolsForWork.Remove(machiningToolForWork);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            var rows = await _db.MachiningToolsForWork
                .Include(m => m.MachiningTool)
                .OrderByDescending(m => m.DateUsed)
                .ToListAsync();
            foreach(MachiningToolForWork machiningToolForWork in rows)
            {
                machiningToolForWork.User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == machiningToolForWork.UserId);
                machiningToolForWork.Work = await _db.Work.FirstOrDefaultAsync(w => w.WorkId == machiningToolForWork.WorkId);
            }
            return rows;
        }

        public async Task<object> GetRow(Guid? id)
        {
            var row = await _db.MachiningToolsForWork
                .Include(m => m.MachiningTool)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
            row.Work = await _db.Work.FirstOrDefaultAsync(w => w.WorkId == row.WorkId);
            row.User = await _db.Users.FirstOrDefaultAsync(u => u.UserId == row.UserId);
            return row;
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.MachiningToolsForWork
                .Include(m => m.MachiningTool)
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.MachiningToolsForWork
                          .AnyAsync(m => m.MachiningToolForWorkId == id);
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var entry = _db.Entry((MachiningToolForWork)obj);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
