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
        public async Task<IActionResult> Create()
        {
            var powderOrder = await _context.PowderOrders.FirstOrDefaultAsync();
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

      

        public ActionResult ManageLineItems(Guid id)
        {
            return RedirectToAction("Index", "LineItems", new { id = id });
        }

        // POST: PowderOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,NumLineItemBottles,VendorId")] PowderOrder powderOrder)
        {
            if (ModelState.IsValid)
            {             
                powderOrder.PowderOrderId = Guid.NewGuid();
                _context.Add(powderOrder);
                var lineItem = new LineItem { LineItemId = Guid.NewGuid(), PowderOrderId = powderOrder.PowderOrderId, NumBottles = powderOrder.NumLineItemBottles };
                _context.Add(lineItem);
                await _context.SaveChangesAsync();
                for(int i = 0; i < powderOrder.NumLineItemBottles; i++)
                {
                    var bottle = new Powder { PowderId = Guid.NewGuid(), LineItemId = lineItem.LineItemId, BottleCost=0, BottleNumber="NA", InitWeight=0, Weight=0, LineItem = lineItem, LotNumber="NA", UserId=new Guid(HttpContext.Session.GetString("userId"))};
                    _context.Add(bottle);
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // POST: PowderOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,NumLineItemBottles,VendorId")] PowderOrder powderOrder)
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", powderOrder.VendorId);
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

        public ActionResult ViewList(Guid? powderOrderId)
        {
            return RedirectToAction("Index", "LineItems", new { powderOrderId = powderOrderId });
        }
        private bool PowderOrderExists(Guid id)
        {
            return _context.PowderOrders.Any(e => e.PowderOrderId == id);
        }
    }
}
