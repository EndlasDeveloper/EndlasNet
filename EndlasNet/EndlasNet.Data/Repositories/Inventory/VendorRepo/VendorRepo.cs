using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class VendorRepo : IVendorRepo
    {
        private readonly EndlasNetDbContext _db;

        public VendorRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Vendor>> GetAllRows()
        {
            return await _db.Vendors
                .OrderByDescending(v => v.VendorName)
                .ToListAsync();
        }

        public async Task<Vendor> GetVendorDetailsAsync(Guid? id)
        {
            return await _db.Vendors
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.VendorId == id);
        }

        public async Task AddRow(Vendor vendor)
        {
            _db.Add(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<Vendor> GetVendorEditAsync(Guid? id)
        {
            return await _db.Vendors.FindAsync(id);
        }

        public async Task UpdateRow(Vendor vendor)
        {
            var entry = _db.Entry(vendor);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Vendor> DeleteVendorAsync(Guid? id)
        {
            return await _db.Vendors
                .FirstOrDefaultAsync(c => c.VendorId == id);
        }

        public async Task DeleteRow(Guid? id)
        {
            var vendor = await _db.Vendors.FindAsync(id);
            _db.Vendors.Remove(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<Vendor> GetRow(Guid? id)
        {
            return await _db.Vendors
                .FirstOrDefaultAsync(v => v.VendorId == id);
        }


        public async Task<Vendor> GetRowNoTracking(Guid? id)
        {
            return await _db.Vendors
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VendorId == id);
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.Vendors
                          .AnyAsync(e => e.VendorId == id);
        }
    }
}