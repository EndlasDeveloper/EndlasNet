using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface ICustomerRepo 
    {
        public Task<IEnumerable<Customer>> GetAllRows();
        public Task<Customer> GetCustomerDetailsAsync(Guid? id);
        public Task<Customer> GetCustomerEditAsync(Guid? id);
        public Task<object> GetRow(Guid? customerId);
        public Task AddRow(Customer customer);
        public Task UpdateRow(Customer customer);
        public Task DeleteRow(Guid? customerId);
        public Task<Customer> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
    }
}
