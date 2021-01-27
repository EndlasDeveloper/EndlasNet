using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAll();

        Task<User> GetUser(string email);

        Task Add(User user);

        Task Update(User user);

        Task Delete(string email);
    }
}
