using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task AddRow(object obj)
        {
            try
            {
                var staticPowder = (StaticPowderInfo)obj;
                await _db.StaticPowderInfo.AddAsync(staticPowder);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            try
            {
                var staticPowder = await _db.StaticPowderInfo
                    .FirstOrDefaultAsync(s => s.StaticPowderInfoId == id);
                _db.StaticPowderInfo.Remove(staticPowder);
                _db.Entry(staticPowder).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
            }
            catch (ArgumentNullException) { }
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.StaticPowderInfo.ToListAsync();
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

        public async Task UpdateRow(object obj)
        {
            try
            {
                var staticPowder = (StaticPowderInfo)obj;
                var entry = _db.Entry(staticPowder);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
