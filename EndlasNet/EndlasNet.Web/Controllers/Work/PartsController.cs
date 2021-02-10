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
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace EndlasNet.Web.Controllers
{
    public class PartsController : Controller
    {

        private readonly EndlasNetDbContext _context;
        private readonly string[] _permittedExtensions = { ".png", ".jpg" };

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
        public async Task<IActionResult> Create([Bind("PartId,DrawingNumber,ConditionDescription,InitWeight,Weight,CladdedWeight,ProcessingNotes,DrawingImage,UserId")] Part part, List<IFormFile> files)
        {

            if (ModelState.IsValid)
            {
                if(files.Count == 1)
                {
                    IFormFile file = files.First();
                    using (var fileStream = file.OpenReadStream())
                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        part.DrawingImage = ms.ToArray();
                        //string s = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                    }
                } else
                {

                    Console.Out.WriteLine("File count failed.");
                }
                part.PartId = Guid.NewGuid();
                //UploadSingle(part);
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

       /* [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }

        */
            /*        [HttpPost]
                    public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
                    {
                        foreach (var file in files)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var extension = Path.GetExtension(file.FileName);
                            var fileModel = new PartModelDb
                            {

                            };
                            using (var dataStream = new MemoryStream())
                            {
                                await file.CopyToAsync(dataStream);
                                fileModel.Data = dataStream.ToArray();
                            }
                            context.FilesOnDatabase.Add(fileModel);
                            context.SaveChanges();
                        }
                        TempData["Message"] = "File successfully uploaded to Database";
                        return RedirectToAction("Index");
                    }*/
            /*
                    public Part UploadSingle(Part part)
                    {
                        using (var fileStream = part.DrawingFormFile.OpenReadStream())
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            part.DrawingImageRaw = ms.ToArray();
                            //string s = Convert.ToBase64String(fileBytes);
                            // act on the Base64 data
                        }
                        return part;
                    }*/
        }
}
