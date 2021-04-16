using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Newtonsoft.Json;

namespace EndlasNet.Web.Models
{
    public class CheckBoxInfo
    {
        public bool IsChecked { get; set; } = false;
        public Guid PartForWorkId { get; set; }
        public string Label { get; set; }
        public string RuntimeId { get; set; }
    }

    public class PowderForPartViewModel
    {
        public Work Work { get; set; }
        public Guid WorkId { get; set; }
        public Guid PowderBottleId { get; set; }
        public float PowderWeightUsed { get; set; }
        public List<CheckBoxInfo> CheckBoxes { get; set; }
    }
    
}
