using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
namespace EndlasNet.Web.Models
{
    public class QuoteDropDownViewModel
    {
        public QuoteDropDownViewModel(Quote quote)
        {
            QuoteId = quote.QuoteId;
            DropDownQuoteDisplayStr = quote.EndlasNumber + "-" + quote.ShortDescription;
        }
        public Guid QuoteId { get; set; }
        public string DropDownQuoteDisplayStr { get; set; }
    }
}
