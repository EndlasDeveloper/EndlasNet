using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class StaticPowderInfoRepo
    {
        private readonly EndlasNetDbContext _db;
        public StaticPowderInfoRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<StaticPowderInfo> GetStaticPowderInfo(Guid? staticPowderInfoId)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == staticPowderInfoId);
        }
    }
}
