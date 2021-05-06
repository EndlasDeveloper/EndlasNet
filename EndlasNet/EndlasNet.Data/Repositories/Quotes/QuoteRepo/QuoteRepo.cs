using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlasNet.Data
{
    public class QuoteRepo : IQuoteRepo
    {
        private readonly EndlasNetDbContext _db;
        public QuoteRepo(EndlasNetDbContext db)
        {
            _db = db;
        }
        public async Task AddRow(Quote quote)
        {
            await _db.Quotes.AddAsync(quote);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRow(Quote quote)
        {
            _db.Quotes.Remove(quote);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quote>> GetAllRows()
        {
            return await _db.Quotes
                .OrderByDescending(q => q.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Quote>> GetDuplicateQuotes(Quote quote)
        {
            return await _db.Quotes
                .Where(q => q.EndlasNumber == quote.EndlasNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Work>> GetDuplicateWork(Quote quote)
        {
            return await _db.Work
                .Where(w => w.EndlasNumber == quote.EndlasNumber)
                .ToListAsync();
        }

        public async Task<Quote> GetRow(Guid id)
        {
            return await _db.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);
        }

        public bool QuoteExists(Guid id)
        {
            return _db.PowderForParts.Any(e => e.PowderForPartId == id);
        }

        public async Task UpdateRow(Quote quote)
        {
            var entry = _db.Entry(quote);
            entry.State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
