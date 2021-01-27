using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Data
{
    public class EmployeeRepo
    {
        private readonly EndlasNetDbContext db;
        public EmployeeRepo(EndlasNetDbContext db)
        {
            this.db = db;
        }

        public async Task Add(Employee employee)
        {
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
        }

        public async Task Delete(string email)
        {
            try
            {
                var user = GetUser(email).Result;
                db.Employees.Remove((Employee)user);
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
            catch (ArgumentNullException) { } // doesn't exist-->need to handle this better
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var employees = await db.Employees.ToListAsync();
            return employees.AsEnumerable();
        }

        public async Task<User> GetUser(string email)
        {
            return await db.Employees.Where(p => p.EndlasEmail == email).FirstOrDefaultAsync();
        }

        public async Task Update(Employee employee)
        {
            var entry = db.Entry(employee);
            entry.State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
