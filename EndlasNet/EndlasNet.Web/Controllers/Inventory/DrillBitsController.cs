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
    public class DrillBitsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public DrillBitsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: DrillBits
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.DrillBits.Include(d => d.User).Include(d => d.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: DrillBits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drillBit = await _context.DrillBits
                .Include(d => d.User)
                .Include(d => d.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
            if (drillBit == null)
            {
                return NotFound();
            }

            return View(drillBit);
        }

        // GET: DrillBits/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: DrillBits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrillBitRadius,MachiningToolId,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] DrillBit drillBit)
        {
            if (ModelState.IsValid)
            {
                drillBit.MachiningToolId = Guid.NewGuid();
                drillBit.UserId = new Guid(HttpContext.Session.GetString("userId"));

                _context.Entry(drillBit).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(drillBit).Property("UpdatedDate").CurrentValue = DateTime.Now;
                drillBit.UserId = new Guid(HttpContext.Session.GetString("userId"));

                _context.Add(drillBit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", drillBit.VendorId);
            return View(drillBit);
        }

        // GET: DrillBits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drillBit = await _context.DrillBits.FindAsync(id);
            if (drillBit == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", drillBit.VendorId);
            return View(drillBit);
        }

        // POST: DrillBits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DrillBitRadius,MachiningToolId,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] DrillBit drillBit)
        {
            if (id != drillBit.MachiningToolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(drillBit).Property("UpdatedDate").CurrentValue = DateTime.Now;
                    _context.Update(drillBit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrillBitExists(drillBit.MachiningToolId))
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", drillBit.VendorId);
            return View(drillBit);
        }

        // GET: DrillBits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drillBit = await _context.DrillBits
                .Include(d => d.User)
                .Include(d => d.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
            if (drillBit == null)
            {
                return NotFound();
            }

            return View(drillBit);
        }

        // POST: DrillBits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drillBit = await _context.DrillBits.FindAsync(id);
            _context.DrillBits.Remove(drillBit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrillBitExists(Guid id)
        {
            return _context.DrillBits.Any(e => e.MachiningToolId == id);
        }
    }
}
