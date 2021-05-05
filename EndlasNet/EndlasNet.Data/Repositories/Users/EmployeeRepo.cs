using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EndlasNetDbContext _db;
        public EmployeeRepo(EndlasNetDbContext db)
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

        public async Task<User> GetRow(Guid? id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.UserId.ToString() == id.ToString());
        }

      
        public async Task<Employee> GetEmployee(Guid employeeId)
        {
            return await _db.Employees
               .FirstOrDefaultAsync(a => a.UserId == employeeId);
        }

        public async Task<IEnumerable<User>> GetAllRows()
        {
            var users = await _db.Users
                .OrderByDescending(u => u.EndlasEmail)
                .ToListAsync();
            return users.AsEnumerable();
        }

     

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _db.Employees
                .OrderByDescending(u => u.EndlasEmail)
                .ToListAsync();
            return employees.AsEnumerable();
        }

        public async Task AddRow(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }


        public async Task AddEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
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

  
        public async Task UpdateEmployee(Employee employee)
        {
            var entry = _db.Entry(employee);
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
