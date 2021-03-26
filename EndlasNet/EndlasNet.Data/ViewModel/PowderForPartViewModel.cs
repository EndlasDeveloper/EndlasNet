using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class CheckBoxInfo 
    {
        public Guid CheckBoxInfoId { get; set; }
        public bool IsChecked { get; set; }
        public Guid PartForWorkId { get; set; }
        public PartForWork PartForWork { get; set; }
    }

    public class PowderForPartViewModel
    {
        public IEnumerable<Work> AllWork { get; set; }
        public Work Work { get; set; }
        public Guid WorkId { get; set; }

        public List<CheckBoxInfo> CheckBoxList { get; set; }

        public float Weight { get; set; }
    }
}
