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
            foreach(PowderForPartViewModel pvm in await _context.PowderForPartViewModels.ToListAsync())
            {
                _context.Remove(pvm);
            }
            await _context.SaveChangesAsync();
            foreach(CheckBoxInfo checkbox in await _context.PowderForPartCheckBoxes.ToListAsync())
            {
                _context.Remove(checkbox);
            }
            await _context.SaveChangesAsync();
            var powderForPartViewModel = new PowderForPartViewModel();
            powderForPartViewModel.AllWork = await _context.Work.ToListAsync();
            ViewData["WorkId"] = new SelectList(powderForPartViewModel.AllWork, "WorkId", "WorkDescription");
            return View(powderForPartViewModel);
        }
        
        public async Task<IActionResult> WorkIsSet([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            powderForPartViewModel.PowderForPartViewModelId = Guid.NewGuid();

           
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.Work.WorkId);
            powderForPartViewModel.WorkId = powderForPartViewModel.Work.WorkId;
            powderForPartViewModel.Work = await _context.Work
                .FirstOrDefaultAsync(w => w.WorkId == powderForPartViewModel.WorkId);
            ViewBag.WorkDescription = powderForPartViewModel.Work.WorkDescription;
            var partsForWork = await _context.PartsForWork.Where(p => p.WorkId == powderForPartViewModel.WorkId).ToListAsync();
            for (int i = 0; i < partsForWork.Count; i++)
            {
                var checkBox = new CheckBoxInfo();
                checkBox.CheckBoxInfoId = Guid.NewGuid();
                checkBox.PartForWork = partsForWork[i];
                checkBox.PartForWorkId = partsForWork[i].PartForWorkId;
                checkBox.IsChecked = false;
                _context.Add(checkBox);
            }
            await _context.SaveChangesAsync();
            
            _context.Add(powderForPartViewModel);
            await _context.SaveChangesAsync();
            powderForPartViewModel.CheckBoxList = await _context.PowderForPartCheckBoxes.ToListAsync();
            return View(powderForPartViewModel);

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
                _context.Add(checkBox);
            }
            await _context.SaveChangesAsync();
            powderForPartViewModel.CheckBoxList = await _context.PowderForPartCheckBoxes.ToListAsync();
            _context.Update(powderForPartViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("WorkIsSet", new { powderForPartViewModel = powderForPartViewModel });
        }

        [HttpPost]
        public IActionResult Create([Bind("AllWork,Work,WorkId,CheckBoxList,Weight")] PowderForPartViewModel powderForPartViewModel)
        {
            var weight = powderForPartViewModel.Weight;
            powderForPartViewModel = _context.PowderForPartViewModels.First();
            powderForPartViewModel.Weight = weight;
            var p = powderForPartViewModel;
            return RedirectToAction("Index", "PowderForPart", new { powderForPartViewModel = powderForPartViewModel });
        }

    }
}
