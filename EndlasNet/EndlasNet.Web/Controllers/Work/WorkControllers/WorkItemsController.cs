using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using EndlasNet.Web.Models;

namespace EndlasNet.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        private readonly IWorkItemRepo _repo;
        public WorkItemsController(IWorkItemRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(Guid workId)
        {
            Work work = await _repo.GetWork(workId);
            var list = await _repo.GetAllRows();
            ViewBag.EndlasNumber = work.EndlasNumber;
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Initialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);

            WorkItemViewModel vm = new WorkItemViewModel { WorkItemId = workItem.WorkItemId, NumPartsForWork = 1, WorkId = workItem.WorkId };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize(Guid id, [Bind("WorkItemId,NumPartsForWork,StaticPartInfoId,WorkId")] WorkItemViewModel vm)
        {
            if (id != vm.WorkItemId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var workItem = await _repo.GetRow(vm.WorkItemId);
                if(workItem == null)
                {
                    return NotFound();
                }

                workItem.IsInitialized = true;
                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Uninitialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);

            return View(workItem);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Uninitialize(Guid id, [Bind("WorkItemId,Work,WorkId")] WorkItem workItem)
        {
            if (id != workItem.WorkItemId)
            {
                return NotFound();
            }
          
            if (ModelState.IsValid)
            {
                workItem.IsInitialized = false;
                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            return View(workItem);
        }

        public IActionResult Edit()
        {
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var workItem = await _repo.GetRow(id);
            if (workItem == null)
                return NotFound();
            return View(workItem);
        }
    }
}
