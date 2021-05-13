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
        public Task<PartForWorkOrder> GetPartForWorkOrderAsync(Guid? id);

        public Task<List<PartForWorkOrder>> GetExistingPartBatch(PartForWorkOrder partForWorkOrder);
        public Task AddPartForWorkOrderAsync(PartForWorkOrder partForWorkOrder);
        public Task DeletePartForWorkOrderConfirmedAsync(Guid id);
        public Task<bool> ConfirmPartForWorkOrderExistsAsync(Guid id);
        public Task<PartForWorkOrder> GetWorkOrderForDeleteAsync(Guid? id);
        public Task UpdatePartForWorkOrderAsync(PartForWorkOrder partForWorkOrder);
        public Task<IEnumerable<PartForWorkOrder>> GetBatch(string workId, string partInfoId);
        public Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo();
        public Task<IEnumerable<WorkOrder>> GetWorkOrdersWithNoParts();
        public Task<IEnumerable<WorkOrder>> GetWorkOrdersWithParts();

        public Task<IEnumerable<WorkOrder>> GetAllWorkOrders();
        public Task<IEnumerable<PartForWorkOrder>> GetPartsForWorkOrdersWithPartInfo(Guid staticPartInfoId);
        public Task DeletePartForWorkOrderAsync(PartForWorkOrder partForWorkOrder);

        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<Work> GetWork(Guid id);
        public Task<PartForWorkOrder> GetPartForWorkOrder(Guid id);
    }
}
