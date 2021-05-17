using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPartForWorkRepo
    {
        public Task<IEnumerable<PartForWork>> GetAllPartsForWork();

        public Task<IEnumerable<PartForJob>> GetAllPartsForJobs();
        public Task<IEnumerable<PartForWorkOrder>> GetAllPartsForWorkOrders();   
        public Task<IEnumerable<PartForWorkOrder>> GetPartForWorkOrders();

        public string GetWorkType(PartForWork partForWork);
        public Task<Customer> GetCustomer(Guid id);
        public Task<PartForWork> GetPartForWork(Guid? id);
        public Task<IEnumerable<StaticPartInfo>> GetAllStaticPartInfo();

        public Task AddPartForWork(PartForWork partForWork);
        public Task UpdatePartForWork(PartForWork partForWork);

        public Task DeletePartForWork(PartForWork partForWork);
        public bool PartForWorkExists(Guid id);
    }
}
