using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public interface IQuoteRepo
    {
        public Task AddRow(Quote quote);
        public Task<Quote> GetRow(Guid id);
        public Task<IEnumerable<Quote>> GetAllRows();
        public Task<IEnumerable<Quote>> GetDuplicateQuotes(Quote quote);
        public Task<IEnumerable<Work>> GetDuplicateWork(Quote quote);
        public Task UpdateRow(Quote quote);
        public Task DeleteRow(Quote quote);
        public bool QuoteExists(Guid id);
    }
}
