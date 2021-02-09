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

        public MachiningToolsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: MachiningTools
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.MachiningTools.Include(m => m.User).Include(m => m.Vendor);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: MachiningTools/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = await _context.MachiningTools
                .Include(m => m.User)
                .Include(m => m.Vendor).AsNoTracking()
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
            if (machiningTool == null)
            {
                return NotFound();
            }

            return View(machiningTool);
        }

        // GET: MachiningTools/Create
        public IActionResult Create()
        {
            PopulateMachiningToolsDropdown();
            return View();
        }

        // POST: MachiningTools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachiningToolId,ToolType,ToolDiameter,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] MachiningTool machiningTool)
        {
            if (ModelState.IsValid)
            {
                machiningTool.MachiningToolId = Guid.NewGuid();
                machiningTool.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Entry(machiningTool).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(machiningTool).Property("UpdatedDate").CurrentValue = DateTime.Now;
                _context.Add(machiningTool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = await _context.MachiningTools.FindAsync(id);
            if (machiningTool == null)
            {
                return NotFound();
            }
            PopulateMachiningToolsDropdown();
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // POST: MachiningTools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MachiningToolId,ToolType,ToolDiameter,VendorDescription,ToolCount,PurchaseOrderNum,PurchaseOrderDate,PurchaseOrderPrice,UserId,VendorId")] MachiningTool machiningTool)
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
                    _context.Entry(machiningTool).Property("UpdatedDate").CurrentValue = DateTime.Now;
                    _context.Update(machiningTool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachiningToolExists(machiningTool.MachiningToolId))
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
            PopulateMachiningToolsDropdown();
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", machiningTool.VendorId);
            return View(machiningTool);
        }

        // GET: MachiningTools/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningTool = await _context.MachiningTools
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MachiningToolId == id);
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
            var machiningTool = await _context.MachiningTools.FindAsync(id);
            _context.MachiningTools.Remove(machiningTool);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachiningToolExists(Guid id)
        {
            return _context.MachiningTools.Any(e => e.MachiningToolId == id);
        }

        private void PopulateMachiningToolsDropdown()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            List<SelectListItem> toolTypes = new List<SelectListItem>() {
            new SelectListItem {
                Text = "Insert", Value = "0"
            },
            new SelectListItem {
                Text = "Drill bit", Value = "1"
            },
            new SelectListItem {
                Text = "Mill tool", Value = "2"
            },

        };
            ViewBag.ToolTypes = toolTypes;
        }
    }
}
