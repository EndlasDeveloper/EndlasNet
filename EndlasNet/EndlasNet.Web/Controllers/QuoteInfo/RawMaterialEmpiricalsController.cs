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
    public class RawMaterialEmpiricalsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public RawMaterialEmpiricalsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: RawMaterialEmpiricals
        public async Task<IActionResult> Index()
        {
            return View(await _context.RawMaterialEmpiricals.ToListAsync());
        }

        // GET: RawMaterialEmpiricals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterialEmpirical = await _context.RawMaterialEmpiricals
                .FirstOrDefaultAsync(m => m.RawMaterialEmpiricalId == id);
            if (rawMaterialEmpirical == null)
            {
                return NotFound();
            }

            return View(rawMaterialEmpirical);
        }

        // GET: RawMaterialEmpiricals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RawMaterialEmpiricals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RawMaterialEmpiricalId,FlowRateSlope,FlowRateYIntercept")] RawMaterialEmpirical rawMaterialEmpirical)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rawMaterialEmpirical);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rawMaterialEmpirical);
        }

        // GET: RawMaterialEmpiricals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterialEmpirical = await _context.RawMaterialEmpiricals.FindAsync(id);
            if (rawMaterialEmpirical == null)
            {
                return NotFound();
            }
            return View(rawMaterialEmpirical);
        }

        // POST: RawMaterialEmpiricals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RawMaterialEmpiricalId,FlowRateSlope,FlowRateYIntercept")] RawMaterialEmpirical rawMaterialEmpirical)
        {
            if (id != rawMaterialEmpirical.RawMaterialEmpiricalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rawMaterialEmpirical);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawMaterialEmpiricalExists(rawMaterialEmpirical.RawMaterialEmpiricalId))
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
            return View(rawMaterialEmpirical);
        }

        // GET: RawMaterialEmpiricals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rawMaterialEmpirical = await _context.RawMaterialEmpiricals
                .FirstOrDefaultAsync(m => m.RawMaterialEmpiricalId == id);
            if (rawMaterialEmpirical == null)
            {
                return NotFound();
            }

            return View(rawMaterialEmpirical);
        }

        // POST: RawMaterialEmpiricals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rawMaterialEmpirical = await _context.RawMaterialEmpiricals.FindAsync(id);
            _context.RawMaterialEmpiricals.Remove(rawMaterialEmpirical);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RawMaterialEmpiricalExists(int id)
        {
            return _context.RawMaterialEmpiricals.Any(e => e.RawMaterialEmpiricalId == id);
        }
    }
}
