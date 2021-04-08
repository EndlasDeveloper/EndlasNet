using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class StaticPartInfoRepo : IStaticPartInfoRepo
    {
        private readonly EndlasNetDbContext _db;

        public StaticPartInfoRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task AddRow(StaticPartInfo staticPartInfo)
        {
            await _db.StaticPartInfo.AddAsync(staticPartInfo);
            await _db.SaveChangesAsync(); 
        }

        public async Task DeleteRow(Guid? id)
        {
            var row = await _db.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
            _db.Remove(row);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<StaticPartInfo>> GetAllRows()
        {
            return await _db.StaticPartInfo
                .Include(s => s.Customer)
                .Include(s => s.User)
                .OrderByDescending(s => s.DrawingNumber)
                .ToListAsync();
        }

        public async Task<StaticPartInfo> GetRow(Guid? id)
        {
            return await _db.StaticPartInfo
                .Include(s => s.Customer)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<StaticPartInfo> GetRowNoTracking(Guid? id)
        {
            return await _db.StaticPartInfo
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.StaticPartInfo
                .AnyAsync(s => s.StaticPartInfoId == id);
        }

        public async Task UpdateRow(StaticPartInfo staticPartInfo)
        {
            var entry = _db.Entry(staticPartInfo);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
