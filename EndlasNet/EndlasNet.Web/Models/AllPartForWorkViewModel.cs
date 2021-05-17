using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class AllPartForWorkViewModel
    {
        public PartForWork PartForWork { get; set; }
        public string WorkDueDate { get; set; }
        public string DisplayDueDate { get; set; }
    }
}
