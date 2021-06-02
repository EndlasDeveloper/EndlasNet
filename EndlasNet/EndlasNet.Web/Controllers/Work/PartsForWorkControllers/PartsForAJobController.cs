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
            List<PartForAWorkViewModel> vmList = new List<PartForAWorkViewModel>();
            foreach(PartForJob partForJob in endlasNetDbContext)
            {

                partForJob.StaticPartInfo = await _repo.GetStaticPartInfo(partForJob.StaticPartInfoId);
                partForJob.Work = await _repo.GetWork(partForJob.WorkId);
                vmList.Insert(0, new PartForAWorkViewModel(partForJob));
            }
            switch (sortOrder)
            {
                case "suffix_desc":
                    vmList = vmList.OrderByDescending(a => a.PartForWork.Suffix).ToList();
                    break;
                case "suffix_asc":
                    vmList = vmList.OrderBy(a => a.PartForWork.Suffix).ToList();
                    break;
                default:
                    break;
            }

            return View(vmList);
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
            return View(partForJob);
        }

        // POST: PartsForAJob/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,PartForWorkImgId")] PartForJob partForJob)
        {
            if (id != partForJob.PartForWorkId)
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
                        

                    partForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    await _repo.UpdatePartForJobAsync(partForJob);
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
                return RedirectToAction("Index", "PartsForAJob", new { id = id, workId = partForJob.WorkId, partInfoId = partForJob.StaticPartInfoId, sortOrder = "suffix_asc" });
            }
            return View(partForJob);
        }

        // GET: PartsForAJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForJob = await _repo.GetPartForJob(id);
            if (partForJob.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForJob.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForJob.PartForWorkImg = partForWorkImg;
            }
            if (partForJob == null)
            {
                return NotFound();
            }
            if(partForJob.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForJob.PartForWorkImg);

            return View(partForJob);
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
