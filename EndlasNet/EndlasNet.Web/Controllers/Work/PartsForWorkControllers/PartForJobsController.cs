using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using EndlasNet.Web.Models;
using static EndlasNet.Web.Helper;

namespace EndlasNet.Web.Controllers
{
    public class PartForJobsController : Controller
    {
        private IPartForJobRepo _repo;

        public PartForJobsController(IPartForJobRepo repo)
        {
            _repo = repo;
        }

        // GET: PartForJob
        public async Task<IActionResult> Index()
        {
            var jobs = await _repo.GetJobsWithParts();
            List<PartsForWorkMinimizedViewModel> vmList = new List<PartsForWorkMinimizedViewModel>();
            foreach(Job job in jobs)
            {
                var partForWork = job.PartsForWork.ToList().FirstOrDefault();
                if (partForWork != null)
                {
                    var vm = new PartsForWorkMinimizedViewModel
                    {
                        WorkId = job.WorkId,
                        PartForWorkId = partForWork.PartForWorkId,
                        StaticPartInfo = partForWork.StaticPartInfo,
                        DrawingNumber = partForWork.StaticPartInfo.DrawingNumber,
                        JobNumber = job.EndlasNumber,
                        PartCount = job.PartsForWork.Count()
                    };
                    FileURL.SetImageURL(vm.StaticPartInfo);
                    vmList.Insert(0, vm);
                }
            }
            return View(vmList);
        }

        // GET: PartForJobs/Create
        public async Task<IActionResult> Create()
        {
            var partForWorkImgs = await _repo.GetAllPartForWorkImgs();
            
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber");
            ViewData["PartForWorkImgId"] = new SelectList(partForWorkImgs, "PartForWorkImgId", "ImageName");
            ViewData["WorkId"] = new SelectList(await _repo.GetJobsWithNoParts(), "WorkId", "EndlasNumber");
            return View();
        }


        // POST: PartForJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartForWorkId,WorkId,StaticPartInfoId,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,NumParts,StartSuffix,UserId,PartForWorkImgId")] PartForJob partForJob)
        {
            var resultList = await _repo.GetPartsForJobsWithPartInfo(partForJob.StaticPartInfoId);
            var count = resultList.Count();
            int max = -1;
            foreach(PartForJob pForJob in resultList)
            {
                var temp = PartSuffixGenerator.SuffixToIndex(pForJob.Suffix);
                if (temp > max)
                    max = temp;
            }
            if (ModelState.IsValid)
            {
                // look to see if this part/job already exists. If so, name suffix from that point
                var existingBatch = await _repo.GetExistingPartBatch(partForJob);
                var initCount = partForJob.NumParts;
                partForJob.NumParts += existingBatch.Count;

                // update the number of parts in each PartForJob
                foreach (PartForJob part in existingBatch)
                {
                    part.NumParts += existingBatch.Count;
                }
                PartForWorkImg partForWorkImg = null;
              
                // create each part for the part batch
                for (int i = count; i < initCount + count; i++)
                {
                    try
                    {
                        var tempPartForJob = partForJob;
                        tempPartForJob.Suffix = Utility.PartSuffixGenerator.IndexToSuffix(i);
                        tempPartForJob.PartForWorkId = Guid.NewGuid();
                        tempPartForJob.UserId = new Guid(HttpContext.Session.GetString("userId"));
                        tempPartForJob.PartForWorkImgId = partForWorkImg.PartForWorkImgId;
                        await _repo .AddPartForJobAsync(tempPartForJob);
                    } catch(Exception ex) { ex.ToString(); continue; }
                }
                var partsForJobs = await _repo.GetAllPartsForJobs();
                foreach(PartForJob part in partsForJobs)
                {
                    part.NumParts = partForJob.NumParts;
                    await _repo.UpdatePartForJobAsync(part);
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["StaticPartInfoId"] = new SelectList(await _repo.GetAllStaticPartInfo(), "StaticPartInfoId", "DrawingNumber", partForJob.StaticPartInfoId);
            ViewData["WorkId"] = new SelectList(await _repo.GetAllJobs(), "WorkId", "EndlasNumber", partForJob.WorkId);
            return View(partForJob);
        }

        public async Task<IActionResult> PartForJobImages()
        {
            var partImages = await _repo.GetAllPartForWorkImgs();
            foreach(PartForWorkImg img in partImages)
            {
                img.ImageUrl = FileURL.GetImageURL(img.ImageBytes);
            }
            return View(await _repo.GetAllPartForWorkImgs());
        }

        public IActionResult PartForWorkImageCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartForWorkImageCreate([Bind("ImageName,ImageFile")] PartForWorkImg partForWorkImg)
        {
            if (ModelState.IsValid)
            {
                partForWorkImg.PartForWorkImgId = Guid.NewGuid();
                if(partForWorkImg.ImageFile != null)
                {
                    partForWorkImg.ImageBytes = await FileURL.GetFileBytes(partForWorkImg.ImageFile);
                }
                await _repo.AddPartForWorkImg(partForWorkImg);
                return RedirectToAction("PartForJobImages");
            }
            return View(partForWorkImg);
        }


        public ActionResult ViewList(Guid? id, Guid workId, Guid partInfoId)
        {
            return RedirectToAction("Index", "PartsForAJob", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "suffix_asc" });
        }

        private async Task<bool> PartForJobExists(Guid id)
        {
            return await _repo.ConfirmPartForJobExistsAsync(id);
        }
    }
}
