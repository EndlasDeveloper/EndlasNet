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
using Microsoft.AspNetCore.Hosting;

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
            // get list of all static part information
            var endlasNetDbContext = await _context.StaticPartInfo
                .Include(s => s.Customer).ToListAsync();
            // setup image url for each row
            foreach (StaticPartInfo partInfo in endlasNetDbContext)
            {
                FileURL.SetImageURL(partInfo);
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
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StaticPartInfoId == id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            // set the part info's image url for rendering
            FileURL.SetImageURL(staticPartInfo);
            ViewBag.id = id;
            ViewBag.HasPdf = staticPartInfo.DrawingPdfBytes;
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
                staticPartInfo.UserId = new Guid(HttpContext.Session.GetString("userId"));
                if (staticPartInfo.ImageFile != null)
                    staticPartInfo.DrawingImageBytes = await FileURL.GetFileBytes(staticPartInfo.ImageFile);
                if (staticPartInfo.DrawingFile != null)
                    staticPartInfo.DrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.DrawingFile);
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
                        staticPartInfo.DrawingImageBytes = await FileURL.GetFileBytes(staticPartInfo.ImageFile);
                    if (staticPartInfo.DrawingFile != null)
                        staticPartInfo.DrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.DrawingFile);
                    staticPartInfo.UserId = new Guid(HttpContext.Session.GetString("userId"));
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
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StaticPartInfoId == id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            if (staticPartInfo.DrawingImageBytes != null)
                FileURL.SetImageURL(staticPartInfo);
            ViewBag.HasPdf = staticPartInfo.DrawingPdfBytes;
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

        [HttpGet]
        public async Task<IActionResult> DownloadPdf(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _context.StaticPartInfo.FirstOrDefaultAsync(s => s.StaticPartInfoId == id);

            if (staticPartInfo.DrawingPdfBytes == null)
            {
                return RedirectToAction("Details", new { id = id });
            }

            var fileName = staticPartInfo.DrawingNumber + "_drawing.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(staticPartInfo.DrawingPdfBytes);
            if(ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName); 
        }
    }
}