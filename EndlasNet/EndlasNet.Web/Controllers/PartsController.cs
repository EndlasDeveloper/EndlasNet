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
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
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
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }
            SetImageURL(part);
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
        public async Task<IActionResult> Create([Bind("PartId,DrawingNumber,ConditionDescription,InitWeight,Weight,CladdedWeight,ProcessingNotes,ImageName,ImageFile,UserId")] Part part)
        {
            if (ModelState.IsValid)
            {
                part.PartId = Guid.NewGuid();
                part.DrawingImage = await FormFileExtenstions.GetBytes(part.ImageFile);
                part.UserId = new Guid(HttpContext.Session.GetString("userId"));
                _context.Entry(part).Property("CreatedDate").CurrentValue = DateTime.Now;
                _context.Entry(part).Property("UpdatedDate").CurrentValue = DateTime.Now;

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
        public async Task<IActionResult> Edit(Guid id, [Bind("PartId,DrawingNumber,ConditionDescription,InitWeight,Weight,CladdedWeight,ProcessingNotes,ImageName,ImageFile,UserId")] Part part)
        {
/*            if (id != part.PartId)
            {
                return NotFound();
            }
*/
            if (ModelState.IsValid)
            {
                try
                {
                    part.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    part.DrawingImage = await FormFileExtenstions.GetBytes(part.ImageFile);
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

        // To convert the Byte Array to the author Image
        public FileContentResult GetImage(byte[] byteArray)
        { 
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        public void SetImageURL(Part part) 
        {
            string imageBase64Data = Convert.ToBase64String(part.DrawingImage);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            ViewBag.ImageData = imageDataURL;
        }
    }
}
