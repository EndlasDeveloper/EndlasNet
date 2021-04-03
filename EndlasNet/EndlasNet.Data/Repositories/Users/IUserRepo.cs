using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IUserRepo : IRepository
    {
        Task<User> GetUser(string email);
        Task<Admin> GetAdmin(Guid adminId);
        Task<Employee> GetEmployee(Guid employeeId);
        string GetUserPrivileges(User user);
        Task AddAdmin(Admin admin);
        Task AddEmployee(Employee employee);
        Task<IEnumerable<Admin>> GetAllAdmins();
        Task<IEnumerable<Employee>> GetAllEmployees();
    }
}
