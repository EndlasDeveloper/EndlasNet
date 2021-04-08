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
        private readonly VendorRepo _vendorRepo;
        private readonly PowderOrderRepo _powderOrderRepo;
        public PowderOrdersController(EndlasNetDbContext context)
        {
            _context = context;
            _vendorRepo = new VendorRepo(context);
            _powderOrderRepo = new PowderOrderRepo(context);
        }

        // GET: PowderOrders
        public async Task<IActionResult> Index()
        {
            return View(await _powderOrderRepo.GetAllRows());
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        public IActionResult ManageLineItems(Guid powderOrderId)
        {
            return RedirectToAction("Index", "LineItems", new { powderOrderId = powderOrderId });
        }

        // POST: PowderOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,VendorId,NumberOfLineItems")] PowderOrder powderOrder)
        {
            if (ModelState.IsValid)
            {             
                powderOrder.PowderOrderId = Guid.NewGuid();
                _context.Add(powderOrder);
                await _context.SaveChangesAsync();
                for(int i = 0; i < powderOrder.NumberOfLineItems; i++)
                {
                    var lineItem = new LineItem()
                    {
                        LineItemId = Guid.NewGuid(),
                        PowderOrder = powderOrder,
                        PowderOrderId = powderOrder.PowderOrderId,
                        VendorDescription = "",
                        Weight = 0,
                        LineItemCost = 0.0f,
                        ParticleSizeMin = 0.0f,
                        ParticleSizeMax = 0.0f,
                        IsInitialized = false
                    };
                    _context.Add(lineItem);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", powderOrder.VendorId);
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
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // POST: PowderOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PowderOrderId,PurchaseOrderNum,PurchaseOrderDate,ShippingCost,TaxCost,VendorId")] PowderOrder powderOrder)
        {
            if (id != powderOrder.PowderOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _powderOrderRepo.UpdateRow(powderOrder);
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
                return RedirectToAction("Index");
            }
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // GET: PowderOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _powderOrderRepo.GetRow(id);

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
            await _powderOrderRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }


        private bool PowderOrderExists(Guid id)
        {
            return _powderOrderRepo.RowExists(id);
        }
    }
}
