using EndlasNet.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Models.DropDownViewModels
{
    public class WorkDropDownViewModel
    {
        public WorkDropDownViewModel(Work work)
        {
            WorkId = work.WorkId;
            DropDownWorkItemDisplayStr = work.EndlasNumber + " - " + work.WorkDescription;
        }
        [Display(Name = "Work")]
        public Guid WorkId { get; set; }
        public string DropDownWorkItemDisplayStr { get; set; }
    }
}
