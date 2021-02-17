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
    public static class FormFileExtenstions
    {
        // returns an IFormFile as a byte array
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
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
            var endlasNetDbContext = _context.StaticPartInfo.Include(s => s.Customer).Include(s => s.User);
            return View(await endlasNetDbContext.ToListAsync());
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StaticPartInfoId == id);
            if (staticPartInfo == null)
            {
                return NotFound();
            }
            SetImageURL(staticPartInfo);
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
        public async Task<IActionResult> Create([Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,ImageFile,CustomerId")] StaticPartInfo staticPartInfo)
        {
            if (ModelState.IsValid)
            {
                staticPartInfo.StaticPartInfoId = Guid.NewGuid();
                staticPartInfo.UserId = new Guid(HttpContext.Session.GetString("userId"));
                staticPartInfo.DrawingImage = await FormFileExtenstions.GetBytes(staticPartInfo.ImageFile);
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // POST: StaticPartInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,DrawingImage,CustomerId,UserId")] StaticPartInfo staticPartInfo)
        {
            if (id != staticPartInfo.StaticPartInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    staticPartInfo.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    staticPartInfo.DrawingImage = await FormFileExtenstions.GetBytes(staticPartInfo.ImageFile);
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
            SetImageURL(staticPartInfo);
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

        public void SetImageURL(StaticPartInfo staticPartInfo)
        {
            string imageBase64Data = Convert.ToBase64String(staticPartInfo.DrawingImage);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            ViewBag.ImageData = imageDataURL;
        }
    }
}
