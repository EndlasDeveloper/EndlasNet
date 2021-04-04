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
        private readonly EndlasNetDbContext _context;
        private readonly MachiningToolRepo _machiningToolRepo;
        private readonly VendorRepo _vendorRepo;
        

        public MachiningToolsController(EndlasNetDbContext context)
        {
            _context = context;
            _vendorRepo = new VendorRepo(context);
            _machiningToolRepo = new MachiningToolRepo(context);
        }

        // GET: MachiningTools
        public async Task<IActionResult> Index()
        {
            return View(await _machiningToolRepo.GetAllRows());
        }

        // GET: MachiningTools/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var machiningTool = (MachiningTool)await _machiningToolRepo.GetRowNoTracking(id);
            if (machiningTool == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", machiningTool.VendorId);

            return View(machiningTool);
        }

        // GET: MachiningTools/Create
        public async Task<IActionResult> Create()
        {
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName");
            return View();
        }

        // POST: MachiningTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachiningToolId,ToolType,ToolDiameter,VendorDescription,InitToolCount,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderCost,InvoiceNumber,UserId,VendorId")] MachiningTool machiningTool)
        {
            if (ModelState.IsValid)
            {
                machiningTool.MachiningToolId = Guid.NewGuid();
                // default tool count to the initial tool count
                machiningTool.ToolCount = machiningTool.InitToolCount;
                machiningTool.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Entry(machiningTool).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(machiningTool).Property("UpdatedDate").CurrentValue = DateTime.Now;
                await _machiningToolRepo.AddRow(machiningTool);
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = await _machiningToolRepo.FindRow(id);
            if (machiningTool == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // POST: MachiningTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MachiningToolId,ToolType,ToolDiameter,VendorDescription,InitToolCount,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderCost,InvoiceNumber,UserId,VendorId")] MachiningTool machiningTool)
        {
            if (id != machiningTool.MachiningToolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    machiningTool.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    await _machiningToolRepo.UpdateRow(machiningTool);
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
            ViewData["VendorId"] = new SelectList(await _vendorRepo.GetAllRows(), "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = (MachiningTool)await _machiningToolRepo.GetRow(id);
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
            await _machiningToolRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MachiningToolExists(Guid id)
        {
            return await _machiningToolRepo.RowExists(id);
        }
    }
}
