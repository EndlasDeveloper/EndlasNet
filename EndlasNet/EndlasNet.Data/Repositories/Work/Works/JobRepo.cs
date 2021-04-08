using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddRow(Job job)
        {
            await _db.Jobs.AddAsync(job);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var job = await _db.Jobs
                .FirstOrDefaultAsync(j => j.WorkId == id);
            _db.Remove(job);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Job>> GetAllRows()
        {
            return await _db.Jobs
                .Include(j => j.Customer)
                .OrderByDescending(j => j.EndlasNumber)
                .ToListAsync();
        }

        public async Task<Job> GetRow(Guid? id)
        {
            return await _db.Jobs
                .Include(j => j.Customer)
                .Include(j => j.User)
                .FirstOrDefaultAsync(j => j.WorkId == id);
        }

        public async Task<Job> GetRowNoTracking(Guid? id)
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

        public async Task UpdateRow(Job job)
        {
            var entry = _db.Entry(job);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Job> FindRow(Guid? id)
        {
            return await _db.Jobs.FindAsync(id);
        }
    }
}
