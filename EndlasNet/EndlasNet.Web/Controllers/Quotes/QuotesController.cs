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
        private readonly IQuoteRepo _repo;
        public QuotesController(IQuoteRepo repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {

            ViewBag.EndlasNumberDescSortParm = String.IsNullOrEmpty(sortOrder) ? "endlas_number_desc" : "";
            ViewBag.EndlasNumberAscSortParm = String.IsNullOrEmpty(sortOrder) ? "endlas_number_asc" : "";

            var quotes = await _repo.GetAllRows();

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

            var quotes = await _repo.GetRow((Guid)id);

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
            var quoteDuplicates = await _repo.GetDuplicateQuotes(quote);
            var workDuplicates = await _repo.GetDuplicateWork(quote);

            if(quoteDuplicates.Count() > 0 || workDuplicates.Count() > 0)
            {
                ViewBag.HasDuplicate = "true";
                ViewBag.EndlasNumber = quote.EndlasNumber;
                return View(quote);
            }

            if (ModelState.IsValid)
            {
                // assign new id
                quote.QuoteId = Guid.NewGuid();
                await _repo.AddRow(quote);
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

            var quote = await _repo.GetRow((Guid)id);
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
            var quoteDuplicates = await _repo.GetDuplicateQuotes(quote);
            var workDuplicates = await _repo.GetDuplicateWork(quote);

            if (quoteDuplicates.Count() > 0 || workDuplicates.Count() > 0)
            {
                ViewBag.HasDuplicate = "true";
                ViewBag.EndlasNumber = quote.EndlasNumber;
                return View(quote);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateRow(quote);
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

            var quotes = await _repo.GetRow((Guid)id);
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
            var quote = await _repo.GetRow((Guid)id);
            await _repo.DeleteRow(quote);
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(Guid id)
        {
            return _repo.QuoteExists(id);
        }
    }
}
