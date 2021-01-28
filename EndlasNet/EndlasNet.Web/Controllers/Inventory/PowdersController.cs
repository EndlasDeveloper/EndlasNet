using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;

namespace EndlasNet.Web.Controllers
{
    public class PowdersController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PowdersController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: Powders
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.Powders.Include(p => p.User).Include(p => p.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: Powders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders
                .Include(p => p.User)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PowderId == id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // GET: Powders/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Powders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderId,PowderName,VendorDescription,PoNumber,PoDate,Weight,CostPerUnitWeight,LotNumber,UserId,VendorId")] Powder powder)
        {
            if (ModelState.IsValid)
            {
                powder.PowderId = Guid.NewGuid();
                powder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Entry(powder).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(powder).Property("UpdatedDate").CurrentValue = DateTime.Now;
                _context.Add(powder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powder.VendorId);
            return View(powder);
        }

        // GET: Powders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders.FindAsync(id);
            if (powder == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powder.VendorId);
            return View(powder);
        }

        // POST: Powders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderId,PowderName,VendorDescription,PoNumber,PoDate,Weight,CostPerUnitWeight,LotNumber,UserId,VendorId")] Powder powder)
        {
            if (id != powder.PowderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowderExists(powder.PowderId))
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powder.VendorId);
            return View(powder);
        }

        // GET: Powders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powder = await _context.Powders
                .Include(p => p.User)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PowderId == id);
            if (powder == null)
            {
                return NotFound();
            }

            return View(powder);
        }

        // POST: Powders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powder = await _context.Powders.FindAsync(id);
            _context.Powders.Remove(powder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowderExists(Guid id)
        {
            return _context.Powders.Any(e => e.PowderId == id);
        }
    }
}
