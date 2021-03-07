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
    public class MachiningToolForWorksController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public MachiningToolForWorksController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: MachiningToolForWorks
        public async Task<IActionResult> Index()
        {
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(_context.MachiningTools, "MachiningToolId", "VendorDescription");

            var toolsForWork = await _context.MachiningToolsForWork.ToListAsync();
            foreach(MachiningToolForWork toolForWork in toolsForWork)
            {
                toolForWork.Work = await _context.Work
                    .FirstOrDefaultAsync(m => m.WorkId == toolForWork.WorkId);
                toolForWork.MachiningTool = await _context.MachiningTools
                    .FirstOrDefaultAsync(m => m.MachiningToolId == toolForWork.MachiningToolId);
            }
            return View(toolsForWork);
        }

        // GET: MachiningToolForWorks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningToolForWork = await _context.MachiningToolsForWork
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
            machiningToolForWork.Work = await _context.Work
                .FirstOrDefaultAsync(m => m.WorkId == machiningToolForWork.WorkId);
            machiningToolForWork.MachiningTool = await _context.MachiningTools
                .FirstOrDefaultAsync(m => m.MachiningToolId == machiningToolForWork.MachiningToolId);
            machiningToolForWork.User = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == machiningToolForWork.UserId);
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Create
        public IActionResult Create()
        {
            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(_context.MachiningTools, "MachiningToolId", "VendorDescription");

            return View();
        }

        // POST: MachiningToolForWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachiningToolForWorkId,DateUsed,WorkId,UserId,MachiningToolId")] MachiningToolForWork machiningToolForWork)
        {
            if (ModelState.IsValid)
            {
                machiningToolForWork.MachiningToolForWorkId = Guid.NewGuid();
                machiningToolForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Add(machiningToolForWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningToolForWork = await _context.MachiningToolsForWork.FirstOrDefaultAsync(m => m.MachiningToolForWorkId==id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
            machiningToolForWork.Work = await _context.Work.FirstOrDefaultAsync(m => m.WorkId == machiningToolForWork.WorkId);
            machiningToolForWork.MachiningTool = await _context.MachiningTools
                .FirstOrDefaultAsync(m => m.MachiningToolId == machiningToolForWork.MachiningToolId);

            ViewData["WorkId"] = new SelectList(_context.Work, "WorkId", "EndlasNumber");
            ViewData["MachiningToolId"] = new SelectList(_context.MachiningTools, "MachiningToolId", "VendorDescription");

            return View(machiningToolForWork);
        }

        // POST: MachiningToolForWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MachiningToolForWorkId,DateUsed,WorkId,UserId,MachiningToolId")] MachiningToolForWork machiningToolForWork)
        {
            if (id != machiningToolForWork.MachiningToolForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    machiningToolForWork.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Update(machiningToolForWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachiningToolForWorkExists(machiningToolForWork.MachiningToolForWorkId))
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
            return View(machiningToolForWork);
        }

        // GET: MachiningToolForWorks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machiningToolForWork = await _context.MachiningToolsForWork
                .FirstOrDefaultAsync(m => m.MachiningToolForWorkId == id);
            if (machiningToolForWork == null)
            {
                return NotFound();
            }
            machiningToolForWork.Work = await _context.Work.FirstOrDefaultAsync(m => m.WorkId == machiningToolForWork.WorkId);
            machiningToolForWork.MachiningTool = await _context.MachiningTools
                .FirstOrDefaultAsync(m => m.MachiningToolId == machiningToolForWork.MachiningToolId);
            machiningToolForWork.User = await _context.Users.FirstOrDefaultAsync(m => m.UserId == machiningToolForWork.UserId);
            return View(machiningToolForWork);
        }

        // POST: MachiningToolForWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var machiningToolForWork = await _context.MachiningToolsForWork.FindAsync(id);
            _context.MachiningToolsForWork.Remove(machiningToolForWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachiningToolForWorkExists(Guid id)
        {
            return _context.MachiningToolsForWork.Any(e => e.MachiningToolForWorkId == id);
        }
    }
}
