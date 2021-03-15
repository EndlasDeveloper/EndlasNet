using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class VendorRepo
    {
        private readonly EndlasNetDbContext _db;

        public VendorRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _db.Vendors.ToListAsync();
        }

        public async Task<Vendor> GetVendorDetailsAsync(Guid? id)
        {
            return await _db.Vendors
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.VendorId == id);
        }

        public async Task AddVendorAsync(Vendor vendor)
        {
            await _db.Vendors.AddAsync(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<Vendor> GetVendorEditAsync(Guid? id)
        {
            return await _db.Vendors.FindAsync(id);
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            _db.Update(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<Vendor> DeleteVendorAsync(Guid? id)
        {
            return await _db.Vendors
                .FirstOrDefaultAsync(c => c.VendorId == id);
        }

        public async Task DeleteVendorConfirmedAsync(Guid? id)
        {
            var vendor = await _db.Vendors.FindAsync(id);
            _db.Vendors.Remove(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ConfirmVendorExistsAsync(Guid id)
        {
            return await _db.Vendors.AnyAsync(e => e.VendorId == id);
        }
    }
}
