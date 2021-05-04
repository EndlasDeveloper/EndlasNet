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
        private readonly StaticPartInfoRepo _staticPartInfoRepo;
        private readonly CustomerRepo _customerRepo;
        private readonly UserRepo _userRepo;
        public StaticPartInfoesController(EndlasNetDbContext context)
        {
            _context = context;
            _userRepo = new UserRepo(context);
            _customerRepo = new CustomerRepo(context);
            _staticPartInfoRepo = new StaticPartInfoRepo(context);
        }

        // GET: StaticPartInfoes
        public async Task<IActionResult> Index()
        {
            // get list of all static part information
            var staticPartInfo = await _staticPartInfoRepo.GetAllRows();
            // setup image url for each row
            foreach (StaticPartInfo partInfo in staticPartInfo)
            {
                FileURL.SetImageURL(partInfo);
            }
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = (StaticPartInfo)await _staticPartInfoRepo.GetRow(id);
            staticPartInfo.User = (User)await _userRepo.GetRow(staticPartInfo.UserId);

            if (staticPartInfo == null)
            {
                return NotFound();
            }
            // set the part info's image url for rendering
            FileURL.SetImageURL(staticPartInfo);
            ViewBag.id = id;
            ViewBag.HasBlankPdf = staticPartInfo.BlankDrawingPdfBytes;
            ViewBag.HasFinishPdf = staticPartInfo.FinishDrawingPdfBytes;
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName");
            return View();
        }

        // POST: StaticPartInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,DrawingImageFile,FinishDrawingFile,BlankDrawingFile,CustomerId")] StaticPartInfo staticPartInfo)
        {
            if (ModelState.IsValid)
            {
                staticPartInfo.StaticPartInfoId = Guid.NewGuid();
                staticPartInfo.UserId = new Guid(HttpContext.Session.GetString("userId"));
                
                if (staticPartInfo.ImageFile != null)
                    staticPartInfo.DrawingImageBytes = await FileURL.GetFileBytes(staticPartInfo.ImageFile);
                if (staticPartInfo.FinishDrawingFile != null)
                    staticPartInfo.FinishDrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.FinishDrawingFile);
                if (staticPartInfo.BlankDrawingFile != null)
                    staticPartInfo.BlankDrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.BlankDrawingFile);

                await _staticPartInfoRepo.AddRow(staticPartInfo);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", staticPartInfo.CustomerId);
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
            if(staticPartInfo.DrawingImageBytes != null)
                FileURL.SetImageURL(staticPartInfo);
            ViewBag.id = id;
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // POST: StaticPartInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StaticPartInfoId,DrawingNumber,ApproxWeight,PartDescription,ImageName,ImageFile,DrawingImageBytes,ClearImg,FinishDrawingFile,FinishDrawingPdfBytes,ClearFinish,BlankDrawingFile,BlankDrawingPdfBytes,ClearBlank,CustomerId,UserId")] StaticPartInfo staticPartInfo)
        {
            if (id != staticPartInfo.StaticPartInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (staticPartInfo.ImageFile != null && !staticPartInfo.ClearImg)
                    {
                        staticPartInfo.DrawingImageBytes = await FileURL.GetFileBytes(staticPartInfo.ImageFile);
                    }
                    else if (staticPartInfo.ClearImg)
                    {
                        staticPartInfo.DrawingImageBytes = null;
                    }

                    if (staticPartInfo.FinishDrawingFile != null)
                        staticPartInfo.FinishDrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.FinishDrawingFile);
                    if (staticPartInfo.ClearFinish)
                        staticPartInfo.FinishDrawingPdfBytes = null;

                    if (staticPartInfo.BlankDrawingFile != null)
                        staticPartInfo.BlankDrawingPdfBytes = await FileURL.GetFileBytes(staticPartInfo.BlankDrawingFile);
                    if (staticPartInfo.ClearBlank)
                        staticPartInfo.BlankDrawingPdfBytes = null;

                    await _staticPartInfoRepo.UpdateRow(staticPartInfo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await StaticPartInfoExists(staticPartInfo.StaticPartInfoId)))
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
            ViewData["CustomerId"] = new SelectList(await _customerRepo.GetAllRows(), "CustomerId", "CustomerName", staticPartInfo.CustomerId);
            return View(staticPartInfo);
        }

        // GET: StaticPartInfoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = await _staticPartInfoRepo.GetRow(id);

            if (staticPartInfo == null)
            {
                return NotFound();
            }
            else if (staticPartInfo.DrawingImageBytes != null)
            {
                FileURL.SetImageURL(staticPartInfo);
            }
            
            ViewBag.id = id;
            ViewBag.HasBlankPdf = staticPartInfo.BlankDrawingPdfBytes;
            ViewBag.HasFinishPdf = staticPartInfo.FinishDrawingPdfBytes;

            return View(staticPartInfo);
        }

        // POST: StaticPartInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _staticPartInfoRepo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StaticPartInfoExists(Guid id)
        {
            return await _staticPartInfoRepo.RowExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFinishPdf(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = (StaticPartInfo)await _staticPartInfoRepo.GetRow(id);
       
            var fileName = staticPartInfo.DrawingNumber + "_finish.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(staticPartInfo.FinishDrawingPdfBytes);
            if(ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName); 
        }

        public async Task<IActionResult> DownloadBlankPdf(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPartInfo = (StaticPartInfo)await _staticPartInfoRepo.GetRow(id);         

            var fileName = staticPartInfo.DrawingNumber + "_blank.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(staticPartInfo.BlankDrawingPdfBytes);
            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }
    }
}