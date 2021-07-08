using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models.DropDownViewModels
{
    public class WorkItemDropDownViewModel
    {
        public WorkItemDropDownViewModel(WorkItem workItem)
        {
            WorkItemId = workItem.WorkItemId;
            DropDownWorkItemDisplayStr = workItem.StaticPartInfo.DrawingNumber + " - " + workItem.StaticPartInfo.PartDescription;
        }
        [Display(Name ="Work item")]
        public Guid WorkItemId { get; set; }
        public string DropDownWorkItemDisplayStr { get; set; }
    }
}
