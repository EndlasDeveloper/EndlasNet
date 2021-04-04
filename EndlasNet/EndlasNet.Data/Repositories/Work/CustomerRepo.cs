using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly EndlasNetDbContext _db;


        public CustomerRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<object>> GetAllRows()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerDetailsAsync(Guid? id)
        {
            return await _db.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> GetCustomerEditAsync(Guid? id)
        {
            return await _db.Customers.FindAsync(id);
        }





        public async Task<object> GetRow(Guid? customerId)
        {
            return await _db.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task AddRow(object obj)
        {
            try
            {
                var customer = (Customer)obj;
                await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public async Task UpdateRow(object obj)
        {
            try
            {
                var customer = (Customer)obj;
                var entry = _db.Entry(customer);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (InvalidCastException) { }
        }

        public Task RemoveRow(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRow(Guid? customerId)
        {
            try
            {
                var customer = await _db.Customers
                    .FirstOrDefaultAsync(u => u.CustomerId == customerId);
                _db.Customers.Remove(customer);
                _db.Entry(customer).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
            }
            catch (ArgumentNullException){ }
        }

        public async Task<object> GetRowNoTracking(Guid? id)
        {
            return await _db.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<bool> RowExists(Guid id)
        {
            return await _db.Customers
                .AnyAsync(c => c.CustomerId == id);
        }
    }
}
