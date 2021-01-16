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
    public class RawMaterial_LaserQuoteSessionController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public RawMaterial_LaserQuoteSessionController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: RawMaterial_LaserQuoteSession
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.RawMaterial_LaserQuoteSessions.Include(r => r.LaserQuoteSession).Include(r => r.RawMaterial);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: RawMaterial_LaserQuoteSession/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterial_LaserQuoteSession = await _context.RawMaterial_LaserQuoteSessions
                .Include(r => r.LaserQuoteSession)
                .Include(r => r.RawMaterial)
                .FirstOrDefaultAsync(m => m.LaserQuoteSessionId == id);
            if (rawMaterial_LaserQuoteSession == null)
            {
                return NotFound();
            }

            return View(rawMaterial_LaserQuoteSession);
        }

        // GET: RawMaterial_LaserQuoteSession/Create
        public IActionResult Create()
        {
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId");
            ViewData["RawMaterialId"] = new SelectList(_context.RawMaterials, "RawMaterialId", "OrderNumber");
            return View();
        }

        // POST: RawMaterial_LaserQuoteSession/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RawMaterialId,LaserQuoteSessionId,PowerInWatts,AvgThicknessIn,SpotSizeMm,PercentBeadOverlap,SurfaceVelocityMmPerSec,PowderRpm,EstCaptureEffeciency,ProcessingFlowRateLiPerMin,LayerSurfaceAreaSqIn")] RawMaterial_LaserQuoteSession rawMaterial_LaserQuoteSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rawMaterial_LaserQuoteSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", rawMaterial_LaserQuoteSession.LaserQuoteSessionId);
            ViewData["RawMaterialId"] = new SelectList(_context.RawMaterials, "RawMaterialId", "OrderNumber", rawMaterial_LaserQuoteSession.RawMaterialId);
            return View(rawMaterial_LaserQuoteSession);
        }

        // GET: RawMaterial_LaserQuoteSession/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterial_LaserQuoteSession = await _context.RawMaterial_LaserQuoteSessions.FindAsync(id);
            if (rawMaterial_LaserQuoteSession == null)
            {
                return NotFound();
            }
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", rawMaterial_LaserQuoteSession.LaserQuoteSessionId);
            ViewData["RawMaterialId"] = new SelectList(_context.RawMaterials, "RawMaterialId", "OrderNumber", rawMaterial_LaserQuoteSession.RawMaterialId);
            return View(rawMaterial_LaserQuoteSession);
        }

        // POST: RawMaterial_LaserQuoteSession/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RawMaterialId,LaserQuoteSessionId,PowerInWatts,AvgThicknessIn,SpotSizeMm,PercentBeadOverlap,SurfaceVelocityMmPerSec,PowderRpm,EstCaptureEffeciency,ProcessingFlowRateLiPerMin,LayerSurfaceAreaSqIn")] RawMaterial_LaserQuoteSession rawMaterial_LaserQuoteSession)
        {
            if (id != rawMaterial_LaserQuoteSession.LaserQuoteSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rawMaterial_LaserQuoteSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawMaterial_LaserQuoteSessionExists(rawMaterial_LaserQuoteSession.LaserQuoteSessionId))
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
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", rawMaterial_LaserQuoteSession.LaserQuoteSessionId);
            ViewData["RawMaterialId"] = new SelectList(_context.RawMaterials, "RawMaterialId", "OrderNumber", rawMaterial_LaserQuoteSession.RawMaterialId);
            return View(rawMaterial_LaserQuoteSession);
        }

        // GET: RawMaterial_LaserQuoteSession/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterial_LaserQuoteSession = await _context.RawMaterial_LaserQuoteSessions
                .Include(r => r.LaserQuoteSession)
                .Include(r => r.RawMaterial)
                .FirstOrDefaultAsync(m => m.LaserQuoteSessionId == id);
            if (rawMaterial_LaserQuoteSession == null)
            {
                return NotFound();
            }

            return View(rawMaterial_LaserQuoteSession);
        }

        // POST: RawMaterial_LaserQuoteSession/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rawMaterial_LaserQuoteSession = await _context.RawMaterial_LaserQuoteSessions.FindAsync(id);
            _context.RawMaterial_LaserQuoteSessions.Remove(rawMaterial_LaserQuoteSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RawMaterial_LaserQuoteSessionExists(int id)
        {
            return _context.RawMaterial_LaserQuoteSessions.Any(e => e.LaserQuoteSessionId == id);
        }
    }
}
