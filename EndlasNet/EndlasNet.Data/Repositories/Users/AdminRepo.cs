using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class AdminRepo : IAdminRepo
    {
        private readonly EndlasNetDbContext _db;
        public AdminRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public string GetUserPrivileges(User user)
        {
            try
            {
                return (string)_db.Entry(user).Property("Discriminator").CurrentValue;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
            return null;
        }
        
        public async Task<Admin> GetAdmin(Guid adminId)
        {
            return await _db.Admins
                .FirstOrDefaultAsync(a => a.UserId == adminId);
        }
 
        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            var admins = await _db.Admins
                .OrderByDescending(u => u.EndlasEmail)
                .ToListAsync();
            return admins.AsEnumerable();
        }

        public async Task AddAdmin(Admin admin)
        {
            _db.Admins.Add(admin);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRow(User user)
        {
            var entry = _db.Entry(user);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRow(Guid id)
        {

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.UserId == id);
            _db.Users.Remove(user);
            _db.Entry(user).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetRowNoTracking(Guid? id)
        {
            return await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.Users
                .AnyAsync(u => u.UserId == id);
        }

        public async Task UpdateAdmin(Admin admin)
        {
            var entry = _db.Entry(admin);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

 
        public async Task<User> DeleteUserAsync(Guid? id)
        {
            return await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> UserExists(Guid id)
        {
            return await _db.Users
                .AnyAsync(u => u.UserId == id);
        }

    }
}
