using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndlasNet.Web.Controllers
{
    public class PowderForPartViewModelController : Controller
    {
        private readonly EndlasNetDbContext _context;

        public PowderForPartViewModelController(EndlasNetDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var powderForPartViewModel = new PowderForPartViewModel();
            powderForPartViewModel.AllWork = await _context.Work.ToListAsync();
            ViewData["WorkId"] = new SelectList(powderForPartViewModel.AllWork, "WorkId", "WorkDescription");
            return View(powderForPartViewModel);
        }

        
        public async Task<IActionResult> WorkIsSet([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.Work.WorkId);
            powderForPartViewModel.WorkId = powderForPartViewModel.Work.WorkId;
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.WorkId);
            ViewBag.WorkDescription = powderForPartViewModel.Work.WorkDescription;
            var partsForWork = await _context.PartsForWork.Where(p => p.WorkId == powderForPartViewModel.WorkId).ToListAsync();
            powderForPartViewModel.CheckBoxList = new List<CheckBoxInfo>();
            for (int i = 0; i < partsForWork.Count; i++)
            {
                var checkBox = new CheckBoxInfo();
                checkBox.CheckBoxInfoId = Guid.NewGuid();
                checkBox.PartForWork = partsForWork[i];
                checkBox.PartForWorkId = partsForWork[i].PartForWorkId;
                checkBox.IsChecked = false;
                powderForPartViewModel.CheckBoxList.Insert(i, checkBox);
            }
            return View(powderForPartViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> WorkIsSetPost([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            powderForPartViewModel.Work = await _context.Work
               .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.Work.WorkId);
            powderForPartViewModel.WorkId = powderForPartViewModel.Work.WorkId;
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.WorkId);
            ViewBag.WorkDescription = powderForPartViewModel.Work.WorkDescription;
            var partsForWork = await _context.PartsForWork.Where(p => p.WorkId == powderForPartViewModel.WorkId).ToListAsync();
            powderForPartViewModel.CheckBoxList = new List<CheckBoxInfo>();
            for (int i = 0; i < partsForWork.Count; i++)
            {
                var checkBox = new CheckBoxInfo();
                checkBox.CheckBoxInfoId = Guid.NewGuid();
                checkBox.PartForWork = partsForWork[i];
                checkBox.PartForWorkId = partsForWork[i].PartForWorkId;
                checkBox.IsChecked = false;
                powderForPartViewModel.CheckBoxList.Insert(i, checkBox);
            }
            return RedirectToAction("WorkIsSet", new { powderForPartViewModel = powderForPartViewModel });
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> SetWork([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.Work.WorkId);
            powderForPartViewModel.WorkId = powderForPartViewModel.Work.WorkId;
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.WorkId);
            ViewBag.WorkDescription = powderForPartViewModel.Work.WorkDescription;
            var partsForWork = await _context.PartsForWork.Where(p => p.WorkId == powderForPartViewModel.WorkId).ToListAsync();
            powderForPartViewModel.CheckBoxList = new List<CheckBoxInfo>();
            for (int i = 0; i < partsForWork.Count; i++)
            {
                var checkBox = new CheckBoxInfo();
                checkBox.CheckBoxInfoId = Guid.NewGuid();
                checkBox.PartForWork = partsForWork[i];
                checkBox.PartForWorkId = partsForWork[i].PartForWorkId;
                checkBox.IsChecked = false;
                powderForPartViewModel.CheckBoxList.Insert(i, checkBox);
            }
                return RedirectToAction("WorkIsSet", new { workId = powderForPartViewModel.WorkId });
        }

        public  IActionResult Create([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            var p = powderForPartViewModel;
            return View();
        }

    }
}
