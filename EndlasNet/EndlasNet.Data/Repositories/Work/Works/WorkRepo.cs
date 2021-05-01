using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class WorkRepo
    {
        protected EndlasNetDbContext _db;
        public WorkRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Work>> GetAllWork()
        {
            return await _db.Work
                .Include(w => w.User)
                .Include(w => w.Quote)
                .Include(w => w.Customer)
                .ToListAsync();
        }

        public async Task<Work> GetWork(Guid? id)
        {
            return await _db.Work
                .Include(w => w.User)
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
    }
}
