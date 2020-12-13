using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public class LaserQuoteSession
    {
        public int LaserQuoteSessionId { get; set; }

        // FK references
        // LaserQuoteSession has 0:many raw material-laser quote sessions
        public IEnumerable<RawMaterial_LaserQuoteSession> RawMat_LasQuoteSes { get; set; }
        public int QuoteSessionId { get; set; }
        public QuoteSession QuoteSession { get; set; }
    }
}
