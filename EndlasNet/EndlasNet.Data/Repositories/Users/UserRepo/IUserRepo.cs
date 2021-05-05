using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IUserRepo
    {
        public Task<User> GetUser(string email);
        public Task<Admin> GetAdmin(Guid adminId);
        public Task<Employee> GetEmployee(Guid employeeId);
        public string GetUserPrivileges(User user);
        public Task AddAdmin(Admin admin);
        public Task AddEmployee(Employee employee);
        public Task<IEnumerable<Admin>> GetAllAdmins();
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<User> GetRowNoTracking(Guid? id);
        public Task<User> DeleteUserAsync(Guid? id);

        public Task UpdateAdmin(Admin admin);
        public Task UpdateEmployee(Employee employee);
        Task DeleteRow(Guid id);
        Task<bool> UserExists(Guid id);
        public Task<bool> RowExists(Guid id);

    }
}
