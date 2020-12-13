﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    // Fixes many to many relationship between raw material and laser quote session
    public class RawMaterial_LaserQuoteSession
    {
        public int RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int LaserQuoteSessionId { get; set; }
        public LaserQuoteSession LaserQuoteSession { get; set; }

    }
}
