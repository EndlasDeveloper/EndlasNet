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
    public class IntermediateParamsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public IntermediateParamsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: IntermediateParams
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.IntermediateParams.Include(i => i.LaserQuoteSession);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: IntermediateParams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intermediateParam = await _context.IntermediateParams
                .Include(i => i.LaserQuoteSession)
                .FirstOrDefaultAsync(m => m.IntermediateParamId == id);
            if (intermediateParam == null)
            {
                return NotFound();
            }

            return View(intermediateParam);
        }

        // GET: IntermediateParams/Create
        public IActionResult Create()
        {
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId");
            return View();
        }

        // POST: IntermediateParams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IntermediateParamId,SurfaceVelocity,StepMm,StepIn,AssumedAvgPassLenIn,PseudoWidthIn,PseudoNumPasses,TimePerBeadSec,TimeBetweenBeadsMin,TimePerLayerMin,CladAddRateSqInPerMin,ApproxVolPerLayerCubicCm,LaserQuoteSessionId")] IntermediateParam intermediateParam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intermediateParam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", intermediateParam.LaserQuoteSessionId);
            return View(intermediateParam);
        }

        // GET: IntermediateParams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intermediateParam = await _context.IntermediateParams.FindAsync(id);
            if (intermediateParam == null)
            {
                return NotFound();
            }
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", intermediateParam.LaserQuoteSessionId);
            return View(intermediateParam);
        }

        // POST: IntermediateParams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IntermediateParamId,SurfaceVelocity,StepMm,StepIn,AssumedAvgPassLenIn,PseudoWidthIn,PseudoNumPasses,TimePerBeadSec,TimeBetweenBeadsMin,TimePerLayerMin,CladAddRateSqInPerMin,ApproxVolPerLayerCubicCm,LaserQuoteSessionId")] IntermediateParam intermediateParam)
        {
            if (id != intermediateParam.IntermediateParamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intermediateParam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntermediateParamExists(intermediateParam.IntermediateParamId))
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
            ViewData["LaserQuoteSessionId"] = new SelectList(_context.LaserQuoteSessions, "LaserQuoteSessionId", "LaserQuoteSessionId", intermediateParam.LaserQuoteSessionId);
            return View(intermediateParam);
        }

        // GET: IntermediateParams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intermediateParam = await _context.IntermediateParams
                .Include(i => i.LaserQuoteSession)
                .FirstOrDefaultAsync(m => m.IntermediateParamId == id);
            if (intermediateParam == null)
            {
                return NotFound();
            }

            return View(intermediateParam);
        }

        // POST: IntermediateParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intermediateParam = await _context.IntermediateParams.FindAsync(id);
            _context.IntermediateParams.Remove(intermediateParam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntermediateParamExists(int id)
        {
            return _context.IntermediateParams.Any(e => e.IntermediateParamId == id);
        }
    }
}
