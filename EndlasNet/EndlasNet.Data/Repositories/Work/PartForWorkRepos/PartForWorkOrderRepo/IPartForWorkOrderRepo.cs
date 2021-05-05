using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPartForWorkOrderRepo
    {
        public Task<List<PartForWorkOrder>> GetAllPartsForWorkOrdersAsync();
        public Task<PartForWorkOrder> GetPartForWorkOrderDetailsAsync(Guid? id);
        public Task<List<PartForWorkOrder>> GetExistingPartBatch(PartForWorkOrder partForWorkOrder);
        public Task AddPartForWorkOrderAsync(PartForWorkOrder partForWorkOrder);
        public Task DeletePartForWorkOrderConfirmedAsync(Guid id);
        public Task<bool> ConfirmPartForWorkOrderExistsAsync(Guid id);
        public Task<PartForWorkOrder> GetCustomerForDeleteAsync(Guid? id);
        public Task UpdatePartForWorkOrderAsync(PartForWorkOrder partForWorkOrder);
        public Task<IEnumerable<PartForWorkOrder>> GetBatch(string workId, string partInfoId);
       
    }
}
