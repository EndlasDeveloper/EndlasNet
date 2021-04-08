﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class CheckBoxInfo
    {
        public bool IsChecked { get; set; } = false;
        public Guid PartForWorkId { get; set; }
    }

    public class PowderForPartViewModel : PowderForPart
    {
        public Work Work { get; set; }
        public IEnumerable<CheckBoxInfo> CheckBoxes { get; set; }
    }
    
}
