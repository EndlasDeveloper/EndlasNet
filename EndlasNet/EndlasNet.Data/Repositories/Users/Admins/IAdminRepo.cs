using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{ 
    public interface IAdminRepo
    {
        public Task<Admin> GetAdmin(Guid adminId);
        public string GetUserPrivileges(User user);
        public Task AddAdmin(Admin admin);
        public Task<IEnumerable<Admin>> GetAllAdmins();
        public Task<User> GetRowNoTracking(Guid? id);
        public Task<User> DeleteUserAsync(Guid? id);

        public Task UpdateAdmin(Admin admin);
        Task DeleteRow(Guid id);
        Task<bool> UserExists(Guid id);
        public Task<bool> RowExists(Guid id);
    }
}
