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
        private readonly IPowderOrderRepo _repo;
        public PowderOrdersController(IPowderOrderRepo repo)
        {
            _repo = repo;
        }

        // GET: PowderOrders
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllRows());
        }

        // GET: PowderOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _repo.GetRow(id);
            if (powderOrder == null)
            {
                return NotFound();
            }

            return View(powderOrder);
        }

        // GET: PowderOrders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName");
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
                await _repo.AddRow(powderOrder);
                List<LineItem> lineItems = new List<LineItem>();
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
                    lineItems.Add(lineItem);
                }
                await _repo.AddLineItems(lineItems);
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // GET: PowderOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _repo.GetRow(id);
            if (powderOrder == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", powderOrder.VendorId);
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
                    await _repo.UpdateRow(powderOrder);
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
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", powderOrder.VendorId);
            return View(powderOrder);
        }

        // GET: PowderOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powderOrder = await _repo.GetRow(id);

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
            await _repo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }


        private bool PowderOrderExists(Guid id)
        {
            return _repo.RowExists(id);
        }
    }
}
