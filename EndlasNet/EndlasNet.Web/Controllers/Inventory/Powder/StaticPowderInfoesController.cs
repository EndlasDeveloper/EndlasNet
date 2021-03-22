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
    public class StaticPowderInfoesController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public StaticPowderInfoesController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: StaticPowderInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StaticPowderInfo.ToListAsync());
        }

        // GET: StaticPowderInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _context.StaticPowderInfo
                .FirstOrDefaultAsync(m => m.StaticPowderInfoId == id);
            if (staticPowderInfo == null)
            {
                return NotFound();
            }

            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaticPowderInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaticPowderInfoId,PowderName,Density,Description,Composition,FlowRateSlope,FlowRateYIntercept")] StaticPowderInfo staticPowderInfo)
        {
            if (ModelState.IsValid)
            {
                staticPowderInfo.StaticPowderInfoId = Guid.NewGuid();
                _context.Add(staticPowderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _context.StaticPowderInfo.FindAsync(id);
            if (staticPowderInfo == null)
            {
                return NotFound();
            }
            return View(staticPowderInfo);
        }

        // POST: StaticPowderInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StaticPowderInfoId,PowderName,Density,Description,Composition,FlowRateSlope,FlowRateYIntercept")] StaticPowderInfo staticPowderInfo)
        {
            if (id != staticPowderInfo.StaticPowderInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staticPowderInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaticPowderInfoExists(staticPowderInfo.StaticPowderInfoId))
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
            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _context.StaticPowderInfo
                .FirstOrDefaultAsync(m => m.StaticPowderInfoId == id);
            if (staticPowderInfo == null)
            {
                return NotFound();
            }

            return View(staticPowderInfo);
        }

        // POST: StaticPowderInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var staticPowderInfo = await _context.StaticPowderInfo.FindAsync(id);
            _context.StaticPowderInfo.Remove(staticPowderInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaticPowderInfoExists(Guid id)
        {
            return _context.StaticPowderInfo.Any(e => e.StaticPowderInfoId == id);
        }
    }
}
