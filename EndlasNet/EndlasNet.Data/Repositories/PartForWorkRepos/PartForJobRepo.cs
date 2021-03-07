using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class PartForJobRepo : IPartForWorkRepo
    {
        private readonly EndlasNetDbContext _db;

        public PartForJobRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<PartForJob>> GetBatch(string workId, string partInfoId)
        {
            var batch = await _db.PartsForJobs.Include(p => p.StaticPartInfo)
                .Include(p => p.User)
                .Include(p => p.Work).ToListAsync();
            batch = (List<PartForJob>)batch.AsEnumerable();
            return batch.Where(p => p.WorkId.ToString() == workId).Where(p => p.StaticPartInfoId.ToString() == partInfoId);
        }
    }
}
