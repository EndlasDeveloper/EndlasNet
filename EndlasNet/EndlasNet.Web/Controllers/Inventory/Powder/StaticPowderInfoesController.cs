using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using System.IO;

namespace EndlasNet.Web.Controllers
{
    public class StaticPowderInfoesController : Controller
    {
        private readonly IStaticPowderInfoRepo _repo;
        public StaticPowderInfoesController(IStaticPowderInfoRepo repo)
        {
            _repo = repo;
        }

        // GET: StaticPowderInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllRows());
        }

        // GET: StaticPowderInfoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _repo.GetRow(id);

            if (staticPowderInfo == null)
            {
                return NotFound();
            }
            ViewBag.HasCompositionPdf = staticPowderInfo.InformationFilePdfBytes;
            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaticPowderInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaticPowderInfoId,EndlasDescription,Density,Description,EstCostPerLb,Composition,FlowRateSlope,FlowRateYIntercept,InformationFile")] StaticPowderInfo staticPowderInfo)
        {
            if (ModelState.IsValid)
            {
                staticPowderInfo.StaticPowderInfoId = Guid.NewGuid();
                if (staticPowderInfo.InformationFile != null)
                    staticPowderInfo.InformationFilePdfBytes = await FileURL.GetFileBytes(staticPowderInfo.InformationFile);
                await _repo.AddRow(staticPowderInfo);
                return RedirectToAction(nameof(Index));
            }
            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _repo.GetRow(id);
            if (staticPowderInfo == null)
            {
                return NotFound();
            }
            return View(staticPowderInfo);
        }

        // POST: StaticPowderInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StaticPowderInfoId,EndlasDescription,Density,Description,EstCostPerLb,Composition,ClearInformation,FlowRateSlope,FlowRateYIntercept,InformationFile")] StaticPowderInfo staticPowderInfo)
        {
            if (id != staticPowderInfo.StaticPowderInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (staticPowderInfo.ClearInformation)
                        staticPowderInfo.InformationFilePdfBytes = null;
                    else if (staticPowderInfo.InformationFile != null)
                        staticPowderInfo.InformationFilePdfBytes = await FileURL.GetFileBytes(staticPowderInfo.InformationFile);
                    await _repo.UpdateRow(staticPowderInfo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await StaticPowderInfoExists(staticPowderInfo.StaticPowderInfoId)))
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
            return View(staticPowderInfo);
        }

        // GET: StaticPowderInfoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _repo.GetRow(id);

            if (staticPowderInfo == null)
            {
                return NotFound();
            }
            ViewBag.HasCompositionPdf = staticPowderInfo.InformationFilePdfBytes;
            return View(staticPowderInfo);
        }

        // POST: StaticPowderInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repo.DeleteRow(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StaticPowderInfoExists(Guid id)
        {
            return await _repo.RowExists(id);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadCompositionPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var staticPowderInfo = await _repo.GetRow(myvar);

            var fileName = staticPowderInfo.EndlasDescription + "_composition.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(staticPowderInfo.InformationFilePdfBytes);

            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }
    }
}
