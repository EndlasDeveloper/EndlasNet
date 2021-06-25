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
    public class PartsForAJobController : Controller
    {
        private IPartForJobRepo _repo;
        private readonly Guid NONE_ID = Guid.Empty;
        public PartsForAJobController(IPartForJobRepo repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> IndexWorkItemBatch(Guid workItemId)
        {
            var list = await _repo.GetWorkItemBatch(workItemId);
            return View(list.ToList());
        }

        // GET: PartsForAJob
        public async Task<IActionResult> Index(Guid partId, Guid workItemId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = partId;
            ViewBag.workId = workItemId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var endlasNetDbContext = await _repo.GetBatch(workItemId.ToString(), partInfoId.ToString());
            foreach (PartForJob partForJob in endlasNetDbContext)
            {

                partForJob.WorkItem.StaticPartInfo = await _repo.GetStaticPartInfo((Guid)partForJob.WorkItem.StaticPartInfoId);
                partForJob.WorkItem.Work = await _repo.GetWork((Guid)partForJob.WorkItem.WorkId);
            }
            switch (sortOrder)
            {
                case "suffix_desc":
                    endlasNetDbContext = endlasNetDbContext.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    endlasNetDbContext = endlasNetDbContext.OrderBy(a => a.Suffix).ToList();
                    break;
                default:
                    break;
            }

            return View(endlasNetDbContext);
        }


        // GET: PartsForAJob/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partForJob = await _repo.GetPartForJobDetailsAsync(id);
           
            if (partForJob == null)
            {
                return NotFound();
            }

            if(partForJob.PartForWorkImgId != NONE_ID && partForJob.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForJob.PartForWorkImg = partForWorkImg;
            }

            ViewBag.id = id;
            ViewBag.workId = partForJob.WorkItem.WorkId;
            ViewBag.partInfoId = partForJob.WorkItem.StaticPartInfoId;
            partForJob = SetImageUrls(partForJob);

            return View(partForJob);
        }

        public IActionResult BackToList(Guid id, Guid workId, Guid partInfoId)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;
            return RedirectToAction("Index", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "" });
        }

        public IActionResult BackToPartBatch(Guid workItemId)
        {
            return RedirectToAction("IndexWorkItemBatch", new { workItemId = workItemId });
        }

        // GET: PartsForAJob/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: PartsForAJob/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _repo.GetPartForJob(id);
            if (partForJob == null)
            {
                return NotFound();
            }
            if (partForJob.PartForWorkImgId != NONE_ID && partForJob.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForJob.PartForWorkImg = partForWorkImg;
            }
            var images = await _repo.GetAllPartForWorkImgs();
            var list = images.ToList();
            var noneImg = new PartForWorkImg { PartForWorkImgId = NONE_ID, ImageName = "None" };
            list.Insert(0, noneImg);
            ViewData["PartForWorkImgId"] = new SelectList(list, "PartForWorkImgId", "ImageName");
            partForJob = SetImageUrls(partForJob);
            return View(partForJob);
        }

        // POST: PartsForAJob/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkItemId,WorkItem,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,Suffix,PartForWorkImg,PartForWorkImgId,ImageFile,ClearImg,MachiningImageFile,ClearMachiningImg,CladdingImageFile,ClearCladdingImg,FinishedImageFile,ClearFinishedImg,UsedImageFile,ClearUsedImg,MachiningImageBytes,CladdingImageBytes,FinishedImageBytes,UsedImageBytes")] PartForJob partForJob)
        {
          if(id != partForJob.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (partForJob.PartForWorkImgId == null || partForJob.PartForWorkImgId == NONE_ID)
                    {
                        partForJob.PartForWorkImg = null;
                        partForJob.PartForWorkImgId = null;
                    }
                    else
                    {
                        partForJob.PartForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                    }
                    partForJob = SetImageUrls(partForJob);
                    partForJob.WorkItem = await _repo.GetWorkItem(partForJob.WorkItemId);
                    partForJob.WorkItem.StaticPartInfo = await _repo.GetStaticPartInfo((Guid)partForJob.WorkItem.StaticPartInfoId);
                    partForJob.WorkItem.Work = await _repo.GetWork((Guid)partForJob.WorkItem.WorkId);
                    partForJob = await SetImageBytes(partForJob);
                    await _repo.UpdatePartForJobAsync((PartForJob)partForJob);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await PartForJobExists(partForJob.PartForWorkId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexWorkItemBatch", "PartsForAJob", new { workItemId = partForJob.WorkItemId });
            }
            return View(partForJob);
        }

        private async Task<PartForJob> SetImageBytes(PartForJob partForJob)
        {
            if (!partForJob.ClearMachiningImg)
            {
                if (partForJob.MachiningImageFile != null)
                {
                    var machiningBytes = await FileURL.GetFileBytes(partForJob.MachiningImageFile);
                    if (machiningBytes != partForJob.MachiningImageBytes)
                    {
                        partForJob.MachiningImageBytes = machiningBytes;
                    }
                }
            }
            else
            {
                partForJob.MachiningImageBytes = null;
            }
            if (!partForJob.ClearCladdingImg)
            {
                if (partForJob.CladdingImageFile != null)
                {
                    var claddingBytes = await FileURL.GetFileBytes(partForJob.CladdingImageFile);
                    if (claddingBytes != partForJob.CladdingImageBytes)
                    {
                        partForJob.CladdingImageBytes = claddingBytes;
                    }
                }
            }
            else
            {
                partForJob.CladdingImageBytes = null;
            }
            if (!partForJob.ClearFinishedImg)
            {

                if (partForJob.FinishedImageFile != null)
                {
                    var finishedBytes = await FileURL.GetFileBytes(partForJob.FinishedImageFile);
                    if (finishedBytes != partForJob.FinishedImageBytes)
                    {
                        partForJob.FinishedImageBytes = finishedBytes;
                    }
                }
            }
            else
            {
                partForJob.FinishedImageBytes = null;
            }
            if (!partForJob.ClearUsedImg)
            {

                if (partForJob.UsedImageFile != null)
                {
                    var usedBytes = await FileURL.GetFileBytes(partForJob.UsedImageFile);
                    if (usedBytes != partForJob.UsedImageBytes)
                    {
                        partForJob.UsedImageBytes = usedBytes;
                    }
                }
            }
            else
            {
                partForJob.UsedImageBytes = null;
            }
            return partForJob;
        }

        // GET: PartsForAJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partForJob = await _repo.GetPartForJob(id);

            if (partForJob == null)
            {
                return NotFound();
            }
            if (partForJob.PartForWorkImgId != null || partForJob.PartForWorkImgId == NONE_ID)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForJob.PartForWorkImg = partForWorkImg;
            }
            SetImageUrls(partForJob);

            return View(partForJob);
        }

        private PartForJob SetImageUrls(PartForJob partForJob)
        {

            if (partForJob.MachiningImageBytes != null)
            {
                partForJob.MachiningImageUrl = FileURL.GetImageURL(partForJob.MachiningImageBytes);
            }
            if (partForJob.CladdingImageBytes != null)
            {
                partForJob.CladdingImageUrl = FileURL.GetImageURL(partForJob.CladdingImageBytes);
            }
            if (partForJob.FinishedImageBytes != null)
            {
                partForJob.FinishedImageUrl = FileURL.GetImageURL(partForJob.FinishedImageBytes);
            }
            if (partForJob.UsedImageBytes != null)
            {
                partForJob.UsedImageUrl = FileURL.GetImageURL(partForJob.UsedImageBytes);
            }
            if (partForJob.PartForWorkImg != null && partForJob.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForJob.PartForWorkImg);
            return partForJob;
        }

        // POST: PartsForAJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForJob = await _repo.GetPartForJob(id);
            await _repo.DeletePartForJobConfirmedAsync(id);
            return RedirectToAction("Index","PartsForAJob",new {id = id, workId = partForJob.WorkItem.WorkId, partInfoId = partForJob.WorkItem.StaticPartInfoId, sortOrder="" });
        }

        public ActionResult RedirectToPartForJob(Guid id)
        {
            return RedirectToAction("Edit", "PartsForAJob", new { id = id });

        }

        private async Task<bool> PartForJobExists(Guid id)
        {
            return await _repo.ConfirmPartForJobExistsAsync(id);
        }
    }
}
