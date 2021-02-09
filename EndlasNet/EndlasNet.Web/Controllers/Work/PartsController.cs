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
using EndlasNet.Web.Models;

namespace EndlasNet.Web.Controllers
{
    public class PartsController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PartsController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET: Parts
        public async Task<IActionResult> Index()
        {
            var endlasNetDbContext = _context.Parts.Include(p => p.User);
            return View(await endlasNetDbContext.ToListAsync());
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.User).AsNoTracking()
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            
            return View(part);
        }

        // GET: Parts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,DrawingNumber,ConditionDescription,InitWeight,Weight,CladdedWeight,ProcessingNotes,DrawingImage,UserId")] Part part)
        {
            if (ModelState.IsValid)
            {
                part.PartId = Guid.NewGuid();
                part.UserId = new Guid(HttpContext.Session.GetString("userId"));

                _context.Entry(part).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(part).Property("UpdatedDate").CurrentValue = DateTime.Now;

                using (var memoryStream = new MemoryStream())
                {
                    part.DrawingImage= memoryStream.ToArray();
                    //await part.DrawingImage.CopyToAsync(memoryStream);

                }
                _context.Add(part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartId,DrawingNumber,ConditionDescription,InitWeight,Weight,CladdedWeight,ProcessingNotes,DrawingImage,UserId")] Part part)
        {
            if (id != part.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    part.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    _context.Entry(part).Property("UpdatedDate").CurrentValue = DateTime.Now;

                    _context.Update(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.PartId))
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
            return View(part);
        }

        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var part = await _context.Parts.FindAsync(id);
            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(Guid id)
        {
            return _context.Parts.Any(e => e.PartId == id);
        }

      /*  public async Task<IActionResult> OnPostUploadAsync()
        {
            BufferedSingleFileUploadDb pi = new BufferedSingleFileUploadDb();
            
            using (var memoryStream = new MemoryStream())
            {
                await BufferedSingleFileUploadDb.FileUpload.FormFile.CopyToAsync(memoryStream);

            // Upload the file if less than 2 MB
            if (memoryStream.Length < 2097152)
            {
                var file = new AppFile()
                {
                    Content = memoryStream.ToArray()
                };

                _dbContext.File.Add(file);

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("File", "The file is too large.");
            }
        }

            return Page();
        }*/
    }
}
