using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EndlasNet.Web.Controllers
{
    public class AllWorkController : Controller
    {
        private readonly IWorkRepo _workRepo;
        public AllWorkController(IWorkRepo repo)
        {
            _workRepo = repo; 
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetWork());
        }


        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _workRepo.GetWork(id);
            work.WorkType = _workRepo.GetWorkType(work);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        private async Task<List<Work>> GetWork()
        {
            var allWork = await _workRepo.GetAllWork();
            foreach (var work in allWork)
            {
                work.WorkType = _workRepo.GetWorkType(work);
            }
            return allWork.ToList();
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _workRepo.GetWork(id);
            work.WorkType = _workRepo.GetWorkType(work);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _workRepo.DeleteWork(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> DownloadProcessPdf(Guid? myvar)
        {
            if (myvar == null)
            {
                return NotFound();
            }

            var job = await _workRepo.GetWork(myvar);

            var fileName = job.EndlasNumber + "_process_notes.pdf";
            Response.ContentType = "application/pdf";
            Response.Headers.Add("content-disposition", "attachment;filename=" + fileName);
            MemoryStream ms = new MemoryStream(job.ProcessSheetNotesPdfBytes);
            if (ms == null)
            {
                return NotFound();
            }
            return File(ms, "application/pdf", fileName);
        }

    }
}
