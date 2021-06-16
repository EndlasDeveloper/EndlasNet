using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using EndlasNet.Web.Models;

namespace EndlasNet.Web.Controllers
{
    public class PartForWorkOrdersController : Controller
    {
        private IPartForWorkOrderRepo _repo;
        private readonly Guid NONE_ID = Guid.Empty;

        public PartForWorkOrdersController(IPartForWorkOrderRepo repo)
        {
            _repo = repo;
        }

        // GET: PartForWorkOrders
        // GET: PartForJob
        public async Task<IActionResult> Index()
        {
            var workOrders = await _repo.GetWorkOrdersWithParts();
            List<PartsForWorkMinimizedViewModel> vmList = new List<PartsForWorkMinimizedViewModel>();
            foreach (var order in workOrders)
            {
                foreach(WorkItem workItem in order.WorkItems)
                {
                    var partForWork = workItem.PartsForWork.ToList().FirstOrDefault();
                    if (partForWork != null)
                    {
                        var vm = new PartsForWorkMinimizedViewModel
                        {
                            WorkId = order.WorkId,
                            PartForWorkId = partForWork.PartForWorkId,
                            StaticPartInfo = partForWork.StaticPartInfo,
                            DrawingNumber = partForWork.StaticPartInfo.DrawingNumber,
                            JobNumber = order.EndlasNumber,
                            PartCount = workItem.PartsForWork.Count()
                        };
                        FileURL.SetImageURL(vm.StaticPartInfo);
                        vmList.Insert(0, vm);
                    }
                }
                
            }
            return View(vmList);
        }

        // GET: PartForWorkOrders/Create
        public async Task<IActionResult> Create()
        {
            var partForWorkImgs = await _repo.GetAllPartForWorkImgs();
            var list = partForWorkImgs.ToList();
            var partForWorkImgNone = new PartForWorkImg
            {
                PartForWorkImgId = NONE_ID,
                ImageName = "None"
            };
            list.Insert(0, partForWorkImgNone);
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber");
            ViewData["PartForWorkImgId"] = new SelectList(list, "PartForWorkImgId", "ImageName");
            ViewData["WorkId"] = new SelectList(await _repo.GetWorkOrdersWithNoParts(), "WorkId", "EndlasNumber");
            return View();
        }

        // POST: PartForWorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId,ClearImg,PartForWorkImgId")] PartForWorkOrder partForWorkOrder)
        {
            // gets list of tools that have count > 0
            var resultList = await _repo.GetPartsForWorkOrdersWithPartInfo(partForWorkOrder.StaticPartInfoId);
            var count = resultList.Count();
            int maxIndex = -1;
            foreach (PartForWorkOrder pForWorkOrder in resultList)
            {
                // convert the suffix to base-10 and find the max converted suffix
                // used to track what suffix a part batch should start with
                var temp = PartSuffixGenerator.SuffixToIndex(pForWorkOrder.Suffix);
                if (temp > maxIndex)
                {
                    maxIndex = temp;
                }
            }
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch(partForWorkOrder);
                // save initial part for work order count
                var initCount = partForWorkOrder.NumParts;
                partForWorkOrder.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForWorkOrder part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }

                // create each part for the part batch starting at max index
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForWorkOrder = partForWorkOrder;
                        // set suffix
                        tempPartForWorkOrder.Suffix = Utility.PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForWorkOrder.PartForWorkId = Guid.NewGuid();
                        // save user email
                        tempPartForWorkOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        if (tempPartForWorkOrder.PartForWorkImgId == NONE_ID)
                            tempPartForWorkOrder.PartForWorkImgId = null;
                        await _repo.AddPartForWorkOrderAsync(tempPartForWorkOrder);
                    }
                    catch (Exception ex) { ex.ToString(); continue; }
                }
                // update the number of parts
                var partsForWorkOrders = await _repo.GetAllPartsForWorkOrdersAsync();
                foreach (PartForWorkOrder part in partsForWorkOrders)
                {
                    part.NumParts = partForWorkOrder.NumParts;
                    await _repo.UpdatePartForWorkOrderAsync(part);
                }
                // success, back to index
                return RedirectToAction(nameof(Index));
            }
            // fail, keep tracking referenced entities and return the part for work order back to the create view
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber", partForWorkOrder.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(await _repo.GetAllWorkOrders(), "WorkId", "EndlasNumber", partForWorkOrder.WorkId);

            return View(partForWorkOrder);
        }

        public ActionResult ViewList(Guid? id, Guid workId, Guid partInfoId)
        {
            return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "suffix_asc" });
        }

        private async Task<bool> PartForWorkOrderExists(Guid id)
        {
            return await _repo.ConfirmPartForWorkOrderExistsAsync(id);
        }
    }
}
