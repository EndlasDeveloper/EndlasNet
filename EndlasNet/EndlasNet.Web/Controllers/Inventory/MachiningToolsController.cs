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
    public class MachiningToolsController : Controller
    {
        private readonly IMachiningToolRepo _repo;
        
        public MachiningToolsController(IMachiningToolRepo repo)
        {
            _repo = repo;
        }

        // GET: MachiningTools
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllRows());
        }

        // GET: MachiningTools/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var machiningTool = (MachiningTool)await _repo.GetRowNoTracking(id);
            if (machiningTool == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", machiningTool.VendorId);

            return View(machiningTool);
        }

        // GET: MachiningTools/Create
        public async Task<IActionResult> Create()
        {
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName");
            return View();
        }

        // POST: MachiningTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachiningToolId,ToolType,ToolDiameter,RadialMetric,Units,ToolDescription,VendorDescription,InitToolCount,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderCost,InvoiceNumber,VendorId")] MachiningTool machiningTool)
        {
            if (ModelState.IsValid)
            {
                machiningTool.MachiningToolId = Guid.NewGuid();
                // default tool count to the initial tool count
                machiningTool.ToolCount = machiningTool.InitToolCount;

                await _repo.AddRow(machiningTool);
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = await _repo.FindRow(id);
            if (machiningTool == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // POST: MachiningTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MachiningToolId,ToolType,ToolDiameter,RadialMetric,Units,ToolDescription,VendorDescription,InitToolCount,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderCost,InvoiceNumber,VendorId")] MachiningTool machiningTool)
        {
            if (id != machiningTool.MachiningToolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repo.UpdateRow(machiningTool);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await MachiningToolExists(machiningTool.MachiningToolId)))
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
            ViewData["VendorId"] = new SelectList(await _repo.GetAllVendors(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = (MachiningTool)await _repo.GetRow(id);
            if (machiningTool == null)
            {
                return NotFound();
            }

            return View(machiningTool);
        }

        // POST: MachiningTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MachiningToolExists(Guid id)
        {
            return await _repo.RowExists(id);
        }
    }
}
