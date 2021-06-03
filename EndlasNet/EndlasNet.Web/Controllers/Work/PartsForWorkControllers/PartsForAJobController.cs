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

        // GET: PartsForAJob
        public async Task<IActionResult> Index(Guid id, Guid workId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var endlasNetDbContext = await _repo.GetBatch(workId.ToString(), partInfoId.ToString());
            foreach(PartForJob partForJob in endlasNetDbContext)
            {

                partForJob.StaticPartInfo = await _repo.GetStaticPartInfo(partForJob.StaticPartInfoId);
                partForJob.Work = await _repo.GetWork(partForJob.WorkId);
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
            if(partForJob.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForJob.PartForWorkImg = partForWorkImg;
            }

            ViewBag.id = id;
            ViewBag.workId = partForJob.WorkId;
            ViewBag.partInfoId = partForJob.StaticPartInfoId;
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
            if (partForJob.PartForWorkImgId != null)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWork,WorkId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,Suffix,StaticPartInfoId,PartForWorkId,PartForWorkImg,PartForWorkImgId,ImageFile,ClearImg,MachiningImageFile,MachiniingClearImg,CladdingImageFile,ClearCladdingImg,FinishedImageFile,ClearFinishedImg,UsedImageFile,NumParts,ClearUsedImg")] PartForJob partForJob)
        {
          if(id != partForJob.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (partForJob.PartForWorkImgId == NONE_ID)
                    {
                        partForJob.PartForWorkImg = null;
                        partForJob.PartForWorkImgId = null;
                    }
                    else
                    {
                        partForJob.PartForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                    }
                    partForJob = SetImageUrls(partForJob);

                    partForJob.StaticPartInfo = await _repo.GetStaticPartInfo(partForJob.StaticPartInfoId);
                    partForJob.Work = await _repo.GetWork(partForJob.PartForWorkId);
                    partForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
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
                return RedirectToAction("Index", "PartsForAJob", new { id = partForJob.PartForWorkId, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder = "suffix_asc" });
            }
            return View(partForJob);
        }

        private async Task<PartForJob> SetImageBytes(PartForJob partForJob)
        {
            if(partForJob.MachiningImageFile != null)
            {
                partForJob.MachiningImageBytes = await FileURL.GetFileBytes(partForJob.MachiningImageFile);
            }
            if (partForJob.CladdingImageFile != null)
            {
                partForJob.CladdingImageBytes = await FileURL.GetFileBytes(partForJob.CladdingImageFile);
            }
            if (partForJob.FinishedImageFile != null)
            {
                partForJob.FinishedImageBytes = await FileURL.GetFileBytes(partForJob.FinishedImageFile);
            }
            if (partForJob.UsedImageFile != null)
            {
                partForJob.UsedImageBytes = await FileURL.GetFileBytes(partForJob.UsedImageFile);
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
            if (partForJob.PartForWorkImgId != null)
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
            if (partForJob.PartForWorkImg.ImageBytes != null)
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
            return RedirectToAction("Index","PartsForAJob",new {id = id, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder="" });
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
