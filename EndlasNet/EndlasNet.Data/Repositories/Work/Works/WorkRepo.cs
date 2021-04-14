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
            return await _db.Work.ToListAsync();
        }

        public async Task<Work> GetWork(Guid id)
        {
            return await _db.Work.FirstOrDefaultAsync(w => w.WorkId == id);
        }
    }
}
