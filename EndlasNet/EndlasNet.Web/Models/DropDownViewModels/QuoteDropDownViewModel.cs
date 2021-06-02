using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class QuoteDropDownViewModel
    {
        public Guid QuoteId { get; set; }
        public string DropDownQuoteDisplayStr { get; set; }
    }
}
