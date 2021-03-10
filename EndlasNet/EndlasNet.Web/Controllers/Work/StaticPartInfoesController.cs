using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EndlasNet.Web.Controllers
{
    public class StaticPartInfoesController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public StaticPartInfoesController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: StaticPartInfoes
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = await _context.StaticPartInfo
                .Include(s => s.Customer).ToListAsync();
            foreach(StaticPartInfo partInfo in endlasNetDbContext)
            {
                ImageURL.SetImageURL(partInfo);
            }
            return View(endlasNetDbContext);
        }

        // GET: StaticPartInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo
                .Include(s => s.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StaticPartInfoId == id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            ImageURL.SetImageURL(staticPartInfo);
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: StaticPartInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,ImageFile,DrawingFile,CustomerId")] StaticPartInfo staticPartInfo)
        {
            if (ModelState.IsValid)
            {
                staticPartInfo.StaticPartInfoId = Guid.NewGuid();
                if (staticPartInfo.ImageFile != null)
                    staticPartInfo.DrawingImage = await ImageURL.GetBytes(staticPartInfo.ImageFile);
                if (staticPartInfo.DrawingFile != null)
                    staticPartInfo.DrawingPDF = await ImageURL.GetBytes(staticPartInfo.DrawingFile);
                _context.Add(staticPartInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo.FindAsync(id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // POST: StaticPartInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,ImageFile,DrawingFile,CustomerId,UserId")] StaticPartInfo staticPartInfo)
        {
            if (id != staticPartInfo.StaticPartInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (staticPartInfo.ImageFile != null)
                        staticPartInfo.DrawingImage = await ImageURL.GetBytes(staticPartInfo.ImageFile);
                    _context.Update(staticPartInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaticPartInfoExists(staticPartInfo.StaticPartInfoId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.StaticPartInfoId == id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            if (staticPartInfo.DrawingImage != null)
                ImageURL.SetImageURL(staticPartInfo);
            return View(staticPartInfo);
        }

        // POST: StaticPartInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var staticPartInfo = await _context.StaticPartInfo.FindAsync(id);
            _context.StaticPartInfo.Remove(staticPartInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaticPartInfoExists(Guid id)
        {
            return _context.StaticPartInfo.Any(e => e.StaticPartInfoId == id);
        }    
    }
}