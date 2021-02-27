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
    public class PowderOrdersController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PowderOrdersController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: PowderOrders
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.PowderOrders.Include(p => p.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: PowderOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _context.PowderOrders
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PowderOrderId == id);
            if (powderOrder == null)
            {
                return NotFound();
            }

            return View(powderOrder);
        }

        // GET: PowderOrders/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "PointOfContact");
            return View();
        }

        // POST: PowderOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,NumLineItems,VendorId")] PowderOrder powderOrder)
        {
            if (ModelState.IsValid)
            {
                powderOrder.PowderOrderId = Guid.NewGuid();
                _context.Add(powderOrder);

                for(int i = 0; i < powderOrder.NumLineItems; i++)
                {
                    var lineItem = new LineItem
                    {
                        LineItemId = Guid.NewGuid(),
                        PowderName = "",
                        VendorDescription = "",
                        ParticleSize = 1,
                        PowderOrder = powderOrder,
                        PowderOrderId = powderOrder.PowderOrderId
                    };
                    _context.Add(lineItem);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // GET: PowderOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _context.PowderOrders.FindAsync(id);
            if (powderOrder == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "PointOfContact", powderOrder.VendorId);
            return View(powderOrder);
        }

        // POST: PowderOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,NumLineItems,VendorId")] PowderOrder powderOrder)
        {
            if (id != powderOrder.PowderOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(powderOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowderOrderExists(powderOrder.PowderOrderId))
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "PointOfContact", powderOrder.VendorId);
            return View(powderOrder);
        }

        // GET: PowderOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _context.PowderOrders
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PowderOrderId == id);
            if (powderOrder == null)
            {
                return NotFound();
            }

            return View(powderOrder);
        }

        // POST: PowderOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var powderOrder = await _context.PowderOrders.FindAsync(id);
            _context.PowderOrders.Remove(powderOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ViewList(Guid id)
        {
            ViewBag.id = id;

            return RedirectToAction("Index", "LineItems", new { id = id });
        }
        private bool PowderOrderExists(Guid id)
        {
            return _context.PowderOrders.Any(e => e.PowderOrderId == id);
        }
    }
}
