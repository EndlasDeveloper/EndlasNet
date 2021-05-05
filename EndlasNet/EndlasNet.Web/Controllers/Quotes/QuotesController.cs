using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Web
{
    public class QuotesController : Controller
    {
        private readonly EndlasNetDbContext _context;
        public QuotesController(EndlasNetDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewBag.EndlasNumberDescSortParm = String.IsNullOrEmpty(sortOrder) ? "endlas_number_desc" : "";
            ViewBag.EndlasNumberAscSortParm = String.IsNullOrEmpty(sortOrder) ? "endlas_number_asc" : "";

            var quotes = await _context.Quotes
                .OrderByDescending(q => q.EndlasNumber)
                .ToListAsync();

            switch (sortOrder)
            {
                case "endlas_number_desc":
                    quotes = quotes.OrderByDescending(q => q.EndlasNumber).ToList();
                    break;
                case "endlas_number_asc":
                    quotes = quotes.OrderByDescending(q => q.EndlasNumber).ToList();
                    quotes.Reverse();
                    break;
                default:
                    break;
            }

            return View(quotes);
        }

        // GET: PowderForParts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);

            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }


        // GET: PowderForParts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowderForParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteId,EndlasNumber,ShortDescription")] Quote quote)
        {
            // 1. check that endlas number is unique in both quotes and work
            var quoteDuplicates = await _context.Quotes.Where(q => q.EndlasNumber == quote.EndlasNumber).ToListAsync();
            var workDuplicates = await _context.Work.Where(w => w.EndlasNumber == quote.EndlasNumber).ToListAsync();

            if(quoteDuplicates.Count > 0 || workDuplicates.Count > 0)
            {
                ViewBag.HasDuplicate = "true";
                ViewBag.EndlasNumber = quote.EndlasNumber;
                return View(quote);
            }

            if (ModelState.IsValid)
            {
                // assign new id
                quote.QuoteId = Guid.NewGuid();
                _context.Add(quote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }
        // GET: PowderForParts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return View(quote);
        }

        // POST: PowderForParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("QuoteId,EndlasNumber,ShortDescription")] Quote quote)
        {
            if (id != quote.QuoteId)
            {
                return NotFound();
            }
            // 1. check that endlas number is unique in both quotes and work
            var quoteDuplicates = await _context.Quotes.Where(q => q.QuoteId != quote.QuoteId).Where(q => q.EndlasNumber == quote.EndlasNumber).ToListAsync();
            var workDuplicates = await _context.Work.Where(w => w.EndlasNumber == quote.EndlasNumber).ToListAsync();

            if (quoteDuplicates.Count > 0 || workDuplicates.Count > 0)
            {
                ViewBag.HasDuplicate = "true";
                ViewBag.EndlasNumber = quote.EndlasNumber;
                return View(quote);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.QuoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }

        // GET: PowderForParts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(q => q.QuoteId == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // POST: PowderForParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(Guid id)
        {
            return _context.PowderForParts.Any(e => e.PowderForPartId == id);
        }
    }
}
