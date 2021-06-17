using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;

namespace EndlasNet.Web.Models
{
    public class WorkItemViewModel
    {
        public Guid WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }

        [Display(Name ="Static part info")]
        public Guid? StaticPartInfoId { get; set; }
        public StaticPartInfo StaticPartInfo { get; set; }
        [Display(Name ="Number of parts")]
        public int NumPartsForWork { get; set; }
        [Display(Name ="Start date")]
        public DateTime? StartDate { get; set; }
        [Display(Name ="Complete date")]
        public DateTime? CompleteDate { get; set; }
        public Guid WorkId { get; set; }

        public WorkItem CombineWorkItemData(WorkItem workItem)
        {

            workItem.StartDate = StartDate;
            workItem.CompleteDate = CompleteDate;
            workItem.StaticPartInfo = StaticPartInfo;
            workItem.StaticPartInfoId = StaticPartInfoId;
            return workItem;
        }

        public void SetupViewModel(WorkItem workItem)
        {
            WorkItem = workItem;
            StaticPartInfoId = workItem.StaticPartInfoId;
            if(workItem.PartsForWork != null)
            {
                NumPartsForWork = workItem.PartsForWork.Count();
            }
            else
            {
                NumPartsForWork = 0;
            }
            StartDate = workItem.StartDate;
            CompleteDate = workItem.CompleteDate;
            WorkId = workItem.Work.WorkId;
        }

    }
}
