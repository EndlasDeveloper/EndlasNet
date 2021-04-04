using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class JobRepo : IWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public JobRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(object obj)
        {
            try
            {
                await _db.Jobs.AddAsync((Job)obj);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var job = await _db.Jobs
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(job);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.Jobs.ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.Customer)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.Jobs
                .AsNoTracking()
                .Include(j => j.Customer)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.Jobs
                        .AnyAsync(j => j.WorkId == id);
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var entry = _db.Entry((User)obj);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task<Job> FindRow(Guid? id)
        {
            return await _db.Jobs.FindAsync(id);
        }
    }
}
