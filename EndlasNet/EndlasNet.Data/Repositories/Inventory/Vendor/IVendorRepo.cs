using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IVendorRepo
    {
        public Task<IEnumerable<Vendor>> GetAllRows();
        public Task<Vendor> GetVendorDetailsAsync(Guid? id);

        public Task AddRow(Vendor vendor);

        public Task<Vendor> GetVendorEditAsync(Guid? id);
        public Task UpdateRow(Vendor vendor);
        public Task<Vendor> DeleteVendorAsync(Guid? id);
        public Task DeleteRow(Guid? id);

        public Task<Vendor> GetRow(Guid? id);

        public Task<Vendor> GetRowNoTracking(Guid? id);

        public Task<bool> RowExists(Guid id);
    }
}
