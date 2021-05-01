using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;

namespace EndlasNet.Web.Controllers
{
    public class AllWorkController : Controller
    {
        private readonly EndlasNetDbContext _context;
        private readonly WorkRepo _workRepo;
        public AllWorkController(EndlasNetDbContext context)
        {
            _context = context;
            _workRepo = new WorkRepo(context);
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
            var allWork = await _context.Work.ToListAsync();
            foreach (var work in allWork)
            {
                work.WorkType = _workRepo.GetWorkType(work);
            }
            return allWork;
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

    }
}
