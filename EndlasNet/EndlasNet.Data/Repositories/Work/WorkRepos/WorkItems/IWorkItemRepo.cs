﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IWorkItemRepo
    {
        public Task<WorkItem> GetRow(Guid? workItemId);
        public Task<IEnumerable<WorkItem>> GetAllRows();
        public Task AddRow(WorkItem workItem);
        public Task UpdateRow(WorkItem workItem);
        public Task DeleteRow(Guid? id);
        public Task<Work> GetWork(Guid workId);

        public Task<IEnumerable<StaticPartInfo>> GetAllPartInfo();
        public Task<IEnumerable<WorkItem>> GetWorkItemsForWork(Guid workId);

        public Task<StaticPartInfo> GetStaticPartInfo(Guid id);
        public Task<IEnumerable<PartForWork>> GetPartsForJobsWithPartInfo(Guid? staticPartInfoId);
        public Task<IEnumerable<PartForWork>> GetExistingPartBatch(Guid workId);
        public Task AddPartForJob(PartForJob partForJob);
    }
}
