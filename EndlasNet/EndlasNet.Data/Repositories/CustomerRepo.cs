using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class CustomerRepo
    {
        private readonly EndlasNetDbContext _db;

        public CustomerRepo(EndlasNetDbContext db)
        {
            _db = db;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _db.Customers.ToListAsync(); 
        }

        public async Task<Customer> GetCustomerDetailsAsync(Guid? id)
        {
            return await _db.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerEditAsync(Guid? id)
        {
            return await _db.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _db.Update(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer> DeleteCustomerAsync(Guid? id)
        {
            return await _db.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }
        
        public async Task DeleteCustomerConfirmedAsync(Guid id)
        {
            var customer = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ConfirmCustomerExists(Guid id)
        {
            return await _db.Customers.AnyAsync(e => e.CustomerId == id);
        }
    }
}
