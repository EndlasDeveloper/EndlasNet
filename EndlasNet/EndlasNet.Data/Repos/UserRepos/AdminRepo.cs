using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Data
{
    public class AdminRepo
    {
        private readonly EndlasNetDbContext db;
        public AdminRepo(EndlasNetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(Admin user)
        {
            await db.Admins.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task Delete(string email)
        {
            try
            {
                var user = GetUser(email).Result;
                db.Admins.Remove((Admin)user);
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            catch (ArgumentNullException) { } // doesn't exist-->need to handle this better
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            var employees = await db.Admins.ToListAsync();
            return employees.AsEnumerable();
        }

        public async Task<Admin> GetUser(string email)
        {
            return await db.Admins.Where(p => p.EndlasEmail == email).FirstOrDefaultAsync();
        }

        public async Task Update(Admin user)
        {
            var entry = db.Entry(user);
            entry.State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
