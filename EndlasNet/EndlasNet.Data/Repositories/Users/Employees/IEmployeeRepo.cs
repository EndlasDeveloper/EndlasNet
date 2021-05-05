using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IEmployeeRepo
    {
        public Task<Employee> GetEmployee(Guid employeeId);
        public string GetUserPrivileges(User user);
        public Task AddEmployee(Employee employee);
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<User> GetRowNoTracking(Guid? id);
        public Task<User> DeleteUserAsync(Guid? id);

        public Task UpdateEmployee(Employee employee);
        Task DeleteRow(Guid id);
        Task<bool> UserExists(Guid id);
        public Task<bool> RowExists(Guid id);
    }
}
