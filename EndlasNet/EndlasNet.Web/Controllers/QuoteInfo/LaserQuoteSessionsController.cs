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
    public class LaserQuoteSessionsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public LaserQuoteSessionsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: LaserQuoteSessions
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.LaserQuoteSessions.Include(l => l.OptionalLaserServices).Include(l => l.QuoteSession);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: LaserQuoteSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laserQuoteSession = await _context.LaserQuoteSessions
                .Include(l => l.OptionalLaserServices)
                .Include(l => l.QuoteSession)
                .FirstOrDefaultAsync(m => m.LaserQuoteSessionId == id);
            if (laserQuoteSession == null)
            {
                return NotFound();
            }

            return View(laserQuoteSession);
        }

        // GET: LaserQuoteSessions/Create
        public IActionResult Create()
        {
            ViewData["OptionalLaserServicesId"] = new SelectList(_context.OptionalLaserServices, "OptionalLaserServicesId", "OptionalLaserServicesId");
            ViewData["QuoteSessionId"] = new SelectList(_context.QuoteSessions, "QuoteSessionId", "QuoteSessionName");
            return View();
        }

        // POST: LaserQuoteSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaserQuoteSessionId,IsFlowRateAnalytical,FinishedPartWeight,NumLayers,NumParts,PartChangeoverTimeHr,PartSurfaceAreaSqIn,SetupTimeMin,ShippingWeightFactor,ArgonCost,EstPowerCost,HourlyLaborRate,HourlyUseRate,FringeRate,ProfitRate,OverheadRate,QuoteSessionId,OptionalLaserServicesId")] LaserQuoteSession laserQuoteSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laserQuoteSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OptionalLaserServicesId"] = new SelectList(_context.OptionalLaserServices, "OptionalLaserServicesId", "OptionalLaserServicesId", laserQuoteSession.OptionalLaserServicesId);
            ViewData["QuoteSessionId"] = new SelectList(_context.QuoteSessions, "QuoteSessionId", "QuoteSessionName", laserQuoteSession.QuoteSessionId);
            return View(laserQuoteSession);
        }

        // GET: LaserQuoteSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laserQuoteSession = await _context.LaserQuoteSessions.FindAsync(id);
            if (laserQuoteSession == null)
            {
                return NotFound();
            }
            ViewData["OptionalLaserServicesId"] = new SelectList(_context.OptionalLaserServices, "OptionalLaserServicesId", "OptionalLaserServicesId", laserQuoteSession.OptionalLaserServicesId);
            ViewData["QuoteSessionId"] = new SelectList(_context.QuoteSessions, "QuoteSessionId", "QuoteSessionName", laserQuoteSession.QuoteSessionId);
            return View(laserQuoteSession);
        }

        // POST: LaserQuoteSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LaserQuoteSessionId,IsFlowRateAnalytical,FinishedPartWeight,NumLayers,NumParts,PartChangeoverTimeHr,PartSurfaceAreaSqIn,SetupTimeMin,ShippingWeightFactor,ArgonCost,EstPowerCost,HourlyLaborRate,HourlyUseRate,FringeRate,ProfitRate,OverheadRate,QuoteSessionId,OptionalLaserServicesId")] LaserQuoteSession laserQuoteSession)
        {
            if (id != laserQuoteSession.LaserQuoteSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laserQuoteSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaserQuoteSessionExists(laserQuoteSession.LaserQuoteSessionId))
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
            ViewData["OptionalLaserServicesId"] = new SelectList(_context.OptionalLaserServices, "OptionalLaserServicesId", "OptionalLaserServicesId", laserQuoteSession.OptionalLaserServicesId);
            ViewData["QuoteSessionId"] = new SelectList(_context.QuoteSessions, "QuoteSessionId", "QuoteSessionName", laserQuoteSession.QuoteSessionId);
            return View(laserQuoteSession);
        }

        // GET: LaserQuoteSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laserQuoteSession = await _context.LaserQuoteSessions
                .Include(l => l.OptionalLaserServices)
                .Include(l => l.QuoteSession)
                .FirstOrDefaultAsync(m => m.LaserQuoteSessionId == id);
            if (laserQuoteSession == null)
            {
                return NotFound();
            }

            return View(laserQuoteSession);
        }

        // POST: LaserQuoteSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laserQuoteSession = await _context.LaserQuoteSessions.FindAsync(id);
            _context.LaserQuoteSessions.Remove(laserQuoteSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaserQuoteSessionExists(int id)
        {
            return _context.LaserQuoteSessions.Any(e => e.LaserQuoteSessionId == id);
        }
    }
}
