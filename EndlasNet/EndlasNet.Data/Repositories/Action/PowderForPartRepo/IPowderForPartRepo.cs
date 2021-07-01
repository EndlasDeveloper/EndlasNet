using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IPowderForPartRepo
    {
        public Task AddRow(PowderForPart powderForPart);
        public Task DeleteRow(Guid? id);
        public Task<IEnumerable<PowderForPart>> GetAllRows();

        public Task<PowderForPart> GetRow(Guid? id);
        public Task<PowderForPart> GetRowNoTracking(Guid? id);
        public Task<bool> RowExists(Guid id);
        public Task UpdateRow(PowderForPart powderForPart);
        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<StaticPowderInfo> GetStaticPowderInfo(Guid id);
        public Task<PowderBottle> GetPowderBottle(Guid id);
        public Task UpdatePowderBottle(PowderBottle powderBottle);
        public Task<PowderForPart> GetPowderForPartWithBottles(Guid id);
        public bool PowderForPartExists(Guid id);
        public Task<IEnumerable<Work>> GetAllWorkWithBottles();
        public Task<Work> GetWork(Guid id);
        public Task<IEnumerable<PartForWork>> GetPartsForWorkSingle(Guid workId);
        public Task<IEnumerable<PartForWork>> GetPartsForWork();
        public Task<IEnumerable<PowderBottle>> GetBottlesWithPowder(float threshold);
        public Task<IEnumerable<PowderBottle>> GetWorkItemBottles(Guid workItemId);
        public Task<IEnumerable<WorkItem>> GetWorkItemsFromWork(Guid workId);
    }
}
