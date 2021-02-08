using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly EndlasNetDbContext db;
        public UserRepo(EndlasNetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task Delete(string email)
        {
            try
            {
                var user = GetUser(email).Result;
                db.Users.Remove(user);
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            catch (ArgumentNullException) { } // doesn't exist-->need to handle this better
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var employees = await db.Users.ToListAsync();
            return employees.AsEnumerable();
        }

        public async Task<User> GetUser(string email)
        {
            return await db.Users.Where(p => p.EndlasEmail == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await db.Users.Where(u => u.UserId.ToString() == userId.ToString()).FirstOrDefaultAsync();
        }

        public async Task Update(User user)
        {
            var entry = db.Entry(user);
            entry.State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
