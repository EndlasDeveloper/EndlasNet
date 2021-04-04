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
    public class VendorsController : Controller
    {
        private UserRepo _userRepo;
        private VendorRepo _vendorRepo;
        public VendorsController(EndlasNetDbContext context)
        {
            _userRepo = new UserRepo(context);
            _vendorRepo = new VendorRepo(context);
        }

        // GET: Vendors
        public async Task<IActionResult> Index()
        {
            return View(await _vendorRepo.GetAllRows());
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vendor = await _vendorRepo.GetVendorDetailsAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,VendorName,PointOfContact,VendorAddress,VendorPhone")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                vendor.VendorId = Guid.NewGuid();
                await _vendorRepo.AddRow(vendor);
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vendor = await _vendorRepo.GetVendorEditAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VendorId,VendorName,PointOfContact,VendorAddress,VendorPhone")] Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vendorRepo.UpdateRow(vendor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await VendorExists(vendor.VendorId)))
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
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _vendorRepo.DeleteVendorAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _vendorRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> VendorExists(Guid id)
        {
            return await _vendorRepo.RowExists(id);
        }
    }
}
