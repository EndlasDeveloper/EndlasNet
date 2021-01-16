using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;

namespace EndlasNet.Web.Controllers
{
    public class QuoteSessionsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public QuoteSessionsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: QuoteSessions
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.QuoteSessions.Include(q => q.Customer);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: QuoteSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteSession = await _context.QuoteSessions
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.QuoteSessionId == id);
            if (quoteSession == null)
            {
                return NotFound();
            }

            return View(quoteSession);
        }

        // GET: QuoteSessions/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            return View();
        }

        // POST: QuoteSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuoteSessionId,QuoteSessionName,QuoteSessionDate,CustomerId")] QuoteSession quoteSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quoteSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", quoteSession.CustomerId);
            return View(quoteSession);
        }

        // GET: QuoteSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteSession = await _context.QuoteSessions.FindAsync(id);
            if (quoteSession == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", quoteSession.CustomerId);
            return View(quoteSession);
        }

        // POST: QuoteSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuoteSessionId,QuoteSessionName,QuoteSessionDate,CustomerId")] QuoteSession quoteSession)
        {
            if (id != quoteSession.QuoteSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quoteSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteSessionExists(quoteSession.QuoteSessionId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", quoteSession.CustomerId);
            return View(quoteSession);
        }

        // GET: QuoteSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteSession = await _context.QuoteSessions
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.QuoteSessionId == id);
            if (quoteSession == null)
            {
                return NotFound();
            }

            return View(quoteSession);
        }

        // POST: QuoteSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quoteSession = await _context.QuoteSessions.FindAsync(id);
            _context.QuoteSessions.Remove(quoteSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteSessionExists(int id)
        {
            return _context.QuoteSessions.Any(e => e.QuoteSessionId == id);
        }
    }
}
