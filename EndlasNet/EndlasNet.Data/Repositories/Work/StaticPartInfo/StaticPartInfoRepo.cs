using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task AddRow(object obj)
        {
            try
            {
                await _db.StaticPartInfo.AddAsync((StaticPartInfo)obj);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task DeleteRow(Guid? id)
        {
            var row = await _db.StaticPartInfo
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
            _db.Remove(row);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.StaticPartInfo.Include(s => s.Customer).Include(s => s.User).ToListAsync();
        }

        public async Task<object> GetRow(Guid? id)
        {
            return await _db.StaticPartInfo.Include(s => s.Customer).Include(s => s.User)
                .FirstOrDefaultAsync(s => s.StaticPartInfoId == id);
        }

        public async Task<object> GetRowNoTracking(Guid? id)
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

        public async Task UpdateRow(object obj)
        {
            try
            {
                _db.Update((StaticPartInfo)obj);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }
    }
}
