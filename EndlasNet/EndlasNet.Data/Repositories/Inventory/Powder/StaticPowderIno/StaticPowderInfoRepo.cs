using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class StaticPowderInfoRepo : IStaticPowderInfoRepo
    {
        private readonly EndlasNetDbContext _db;
        public StaticPowderInfoRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(StaticPowderInfo staticPowder)
        {
            await _db.StaticPowderInfo.AddAsync(staticPowder);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Guid? id)
        {
            var staticPowderInfo = await _db.StaticPowderInfo.FindAsync(id);
            _db.StaticPowderInfo.Remove(staticPowderInfo);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<StaticPowderInfo>> GetAllRows()
        {
            return await _db.StaticPowderInfo
                .OrderByDescending(s => s.PowderName)
                .ToListAsync();
        }

        public async Task<StaticPowderInfo> GetRow(Guid? id)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.StaticPowderInfo
                           .AsNoTracking()
                           .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task<StaticPowderInfo> GetStaticPowderInfo(Guid? staticPowderInfoId)
        {
            return await _db.StaticPowderInfo
                .FirstOrDefaultAsync(s => s.StaticPowderInfoId == staticPowderInfoId);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.StaticPowderInfo
                .AnyAsync(s => s.StaticPowderInfoId == id);
        }

        public async Task UpdateRow(StaticPowderInfo staticPowder)
        {
            var entry = _db.Entry(staticPowder);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync(); 
        }
    }
}
