using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
using EndlasNet.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace EndlasNet.Web.Controllers
{
    public class WorkItemsController : Controller
    {
        private readonly IWorkItemRepo _repo;
        private readonly Guid NONE_ID = Guid.Empty;

        public WorkItemsController(IWorkItemRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index(Guid workId)
        {
            Work work = await _repo.GetWork(workId);
            ViewBag.EndlasNumber = work.EndlasNumber;
            return View(work.WorkItems);
        }

        [HttpGet]
        public async Task<IActionResult> Initialize(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = await CreateWorkItemViewModel(id);
            
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Initialize(Guid id, [Bind("WorkItemId,WorkItem,StaticPartInfoId,NumPartsForWork,StartDate,CompleteDate,WorkId")] WorkItemViewModel vm)
        {
            if (id != vm.WorkItemId)
            {
                return NotFound();
            }
            var resultList = await _repo.GetPartsForJobsWithPartInfo(vm.StaticPartInfoId);
            var count = resultList.Count();
            int max = 0;
            foreach (PartForJob pForJob in resultList)
            {
                var temp = PartSuffixGenerator.SuffixToIndex(pForJob.Suffix);
                if (temp > max)
                    max = temp;
            }

            if (ModelState.IsValid)
            {
                var workItem = await _repo.GetRow(vm.WorkItemId);
                workItem.StaticPartInfoId = vm.StaticPartInfoId;
                workItem.StartDate = vm.StartDate;
                workItem.CompleteDate = vm.CompleteDate;
               
                workItem.IsInitialized = true;
      
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch((Guid)workItem.WorkId);
                var initCount = vm.NumPartsForWork;
                vm.NumPartsForWork += existingBatch.Count();

                // update the number of parts in each PartForJob
                foreach (PartForJob part in existingBatch)
                {
                    vm.NumPartsForWork += existingBatch.Count();
                }


                // create each part for the part batch
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForJob = new PartForJob { PartForWorkId = Guid.NewGuid(), WorkItemId = workItem.WorkItemId,  };
                        tempPartForJob.Suffix = Utility.PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        if (tempPartForJob.PartForWorkImgId == NONE_ID)
                            tempPartForJob.PartForWorkImgId = null;

                        await _repo.AddPartForJob(tempPartForJob);
                    }
                    catch (Exception ex) { ex.ToString(); continue; }
                }
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

            var vm = await CreateWorkItemViewModel(id);
            FileURL.SetImageURL(vm.WorkItem.StaticPartInfo);
            if (vm.WorkItem.WorkItemImageBytes != null)
                vm.WorkItem.WorkItemImageUrl = FileURL.GetImageURL(vm.WorkItem.WorkItemImageBytes);
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Uninitialize(Guid id)
        {
            var workItem = await _repo.GetRow(id);
            workItem.IsInitialized = false;
            await _repo.DeletePartBatch(workItem.PartsForWork.ToList());
            await _repo.DeleteRow(id);
            await _repo.AddRow(workItem);
            return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workItem = await _repo.GetRow(id);

            if (workItem.WorkItemImageBytes!= null)
                workItem.WorkItemImageUrl = FileURL.GetImageURL(workItem.WorkItemImageBytes);
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");
            return View(workItem);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("WorkItemId,StaticPartInfoId,StartDate,CompleteDate,WorkId,WorkItemImageFile,WorkItemImageBytes,ClearWorkItemImg,IsInitialized")] WorkItem workItem)
        {
            if (id != workItem.WorkItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (workItem.WorkItemImageFile!= null && !workItem.ClearWorkItemImg)
                {
                    workItem.WorkItemImageBytes = await FileURL.GetFileBytes(workItem.WorkItemImageFile);
                }
                else if (workItem.ClearWorkItemImg)
                {
                    workItem.WorkItemImageBytes = null;
                }

                await _repo.UpdateRow(workItem);
                return RedirectToAction("Index", "WorkItems", new { workId = workItem.WorkId });
            }
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllPartInfo(), "StaticPartInfoId", "PartDescription");

            return View(workItem);

        }
        public async Task<IActionResult> Details(Guid id)
        {
            var workItem = await _repo.GetRow(id);

            if (workItem == null)
            {
                return NotFound();
            }
            var vm = await CreateWorkItemViewModel(id);
            FileURL.SetImageURL(vm.WorkItem.StaticPartInfo);
            if(vm.WorkItem.WorkItemImageBytes != null)
                vm.WorkItem.WorkItemImageUrl = FileURL.GetImageURL(vm.WorkItem.WorkItemImageBytes);
            return View(vm);
        }

        public IActionResult ManagePartsForWork(Guid workItemId)
        {
            return RedirectToAction("IndexWorkItemBatch", "PartsForAJob", new {  workItemId = workItemId });
        }

        private async Task<WorkItemViewModel> CreateWorkItemViewModel(Guid? id)
        {
            var workItem = await _repo.GetRow(id);
            WorkItemViewModel vm = new WorkItemViewModel
            {
                WorkItemId = workItem.WorkItemId,
                WorkItem = workItem,
                WorkId = workItem.Work.WorkId
            };
            if(workItem.PartsForWork != null || workItem.PartsForWork.Count() > 0)
            {
                vm.NumPartsForWork = workItem.PartsForWork.Count();
            }
            if(workItem.StartDate != null)
            {
                vm.StartDate = workItem.StartDate;
            }
            if(workItem.CompleteDate != null)
            {
                vm.CompleteDate = workItem.CompleteDate;
            }
            return vm;

        }
    }
}
