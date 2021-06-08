using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;

namespace EndlasNet.Web.Controllers
{
    public class PartsForAWorkOrderController : Controller
    {
        private IPartForWorkOrderRepo _repo;
        private readonly Guid NONE_ID = Guid.Empty;
        public PartsForAWorkOrderController(IPartForWorkOrderRepo repo)
        {
            _repo = repo;
        }

        // GET: PartsForAWorkOrder
        public async Task<IActionResult> Index(Guid id, Guid workId, Guid partInfoId, string sortOrder)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;

            ViewBag.SuffixDescSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_desc" : "";
            ViewBag.SuffixAscSortParm = String.IsNullOrEmpty(sortOrder) ? "suffix_asc" : "";

            var partBatchList = await _repo.GetBatch(workId.ToString(), partInfoId.ToString());
            foreach (PartForWorkOrder partForWorkOrder in partBatchList)
            {
                partForWorkOrder.StaticPartInfo = await _repo.GetStaticPartInfo(partForWorkOrder.StaticPartInfoId);
                partForWorkOrder.Work = await _repo.GetWork((Guid)partForWorkOrder.WorkId);
            }
            switch (sortOrder)
            {
                case "suffix_desc":
                    partBatchList = partBatchList.OrderByDescending(a => a.Suffix).ToList();
                    break;
                case "suffix_asc":
                    partBatchList = partBatchList.OrderByDescending(a => a.Suffix).ToList();
                    partBatchList = partBatchList.Reverse();
                    break;
                default:
                    break;
            }

            return View(partBatchList);
        }
        // GET: PartsForAWorkOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _repo.GetPartForWorkOrderDetailsAsync(id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }

            if (partForWorkOrder.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForWorkOrder.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForWorkOrder.PartForWorkImg = partForWorkImg;
            }

            ViewBag.id = id;
            ViewBag.workId = partForWorkOrder.WorkId;
            ViewBag.partInfoId = partForWorkOrder.StaticPartInfoId;
            partForWorkOrder = await SetImageUrls(partForWorkOrder);
            return View(partForWorkOrder);
        }

        public IActionResult BackToList(Guid id, Guid workId, Guid partInfoId)
        {
            ViewBag.id = id;
            ViewBag.workId = workId;
            ViewBag.partInfoId = partInfoId;
            return RedirectToAction("Index", new { id = id, workId = workId, partInfoId = partInfoId, sortOrder = "" });
        }

        // GET: PartsForAWorkOrder/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _repo.GetPartForWorkOrder((Guid)id);
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            if (partForWorkOrder.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForWorkOrder.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForWorkOrder.PartForWorkImg = partForWorkImg;
            }
            var images = await _repo.GetAllPartForWorkImgs();
            var list = images.ToList();
            var noneImg = new PartForWorkImg { PartForWorkImgId = NONE_ID, ImageName = "None" };
            list.Insert(0, noneImg);
            ViewData["PartForWorkImgId"] = new SelectList(list, "PartForWorkImgId", "ImageName");
            partForWorkOrder = await SetImageUrls (partForWorkOrder);
            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PartForWorkId,WorkId,StaticPartInfoId,Suffix,NumParts,ConditionDescription,InitWeight,CladdedWeight,FinishedWeight,ProcessingNotes,UserId,PartForWorkImgId")] PartForWorkOrder partForWorkOrder)
        {
            if (id != partForWorkOrder.PartForWorkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (partForWorkOrder.PartForWorkImgId == NONE_ID)
                    {
                        partForWorkOrder.PartForWorkImg = null;
                        partForWorkOrder.PartForWorkImgId = null;
                    }
                    else
                    {
                        partForWorkOrder.PartForWorkImg = await _repo.GetPartForWorkImg((Guid)partForWorkOrder.PartForWorkImgId);
                    }
                    partForWorkOrder = await SetImageUrls(partForWorkOrder);

                    partForWorkOrder.StaticPartInfo = await _repo.GetStaticPartInfo(partForWorkOrder.StaticPartInfoId);
                    partForWorkOrder.Work = await _repo.GetWork(partForWorkOrder.PartForWorkId);
                    partForWorkOrder.UserId = new Guid(HttpContext.Session.GetString("userId"));
                    await _repo.UpdatePartForWorkOrderAsync(partForWorkOrder);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await PartForWorkOrderExists(partForWorkOrder.PartForWorkId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = partForWorkOrder.WorkId, partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "suffix_asc" });
            }
            return View(partForWorkOrder);
        }

        // GET: PartsForAWorkOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partForWorkOrder = await _repo.GetPartForWorkOrder((Guid)id);
            if (partForWorkOrder.PartForWorkImgId != null)
            {
                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)partForWorkOrder.PartForWorkImgId);
                FileURL.SetImageURL(partForWorkImg);
                partForWorkOrder.PartForWorkImg = partForWorkImg;
            }
            if (partForWorkOrder == null)
            {
                return NotFound();
            }
            if (partForWorkOrder.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkOrder.PartForWorkImg);
            partForWorkOrder = await SetImageUrls (partForWorkOrder);
            return View(partForWorkOrder);
        }

        // POST: PartsForAWorkOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWorkOrder = await _repo.GetPartForWorkOrder(id);
            await _repo.DeletePartForWorkOrderAsync(partForWorkOrder);
            return RedirectToAction("Index", "PartsForAWorkOrder", new { id = id, workId = partForWorkOrder.WorkId,
                partInfoId = partForWorkOrder.StaticPartInfoId, sortOrder = "" });
        }

        public ActionResult RedirectToPartForWorkOrder(Guid id)
        {
            return RedirectToAction("Edit", "PartsForAWorkOrder", new { id = id });
        }

        private async Task<bool> PartForWorkOrderExists(Guid id)
        {
            return await _repo.ConfirmPartForWorkOrderExistsAsync(id);
        }

        private async Task<PartForWorkOrder> SetImageBytes(PartForWorkOrder partForWorkOrder)
        {
            if (!partForWorkOrder.ClearMachiningImg)
            {
                if (partForWorkOrder.MachiningImageFile != null)
                {
                    var machiningBytes = await FileURL.GetFileBytes(partForWorkOrder.MachiningImageFile);
                    if (machiningBytes != partForWorkOrder.MachiningImageBytes)
                    {
                        partForWorkOrder.MachiningImageBytes = machiningBytes;
                    }
                }
            }
            else
            {
                partForWorkOrder.MachiningImageBytes = null;
            }
            if (!partForWorkOrder.ClearCladdingImg)
            {
                if (partForWorkOrder.CladdingImageFile != null)
                {
                    var claddingBytes = await FileURL.GetFileBytes(partForWorkOrder.CladdingImageFile);
                    if (claddingBytes != partForWorkOrder.CladdingImageBytes)
                    {
                        partForWorkOrder.CladdingImageBytes = claddingBytes;
                    }
                }
            }
            else
            {
                partForWorkOrder.CladdingImageBytes = null;
            }
            if (!partForWorkOrder.ClearFinishedImg)
            {

                if (partForWorkOrder.FinishedImageFile != null)
                {
                    var finishedBytes = await FileURL.GetFileBytes(partForWorkOrder.FinishedImageFile);
                    if (finishedBytes != partForWorkOrder.FinishedImageBytes)
                    {
                        partForWorkOrder.FinishedImageBytes = finishedBytes;
                    }
                }
            }
            else
            {
                partForWorkOrder.FinishedImageBytes = null;
            }
            if (!partForWorkOrder.ClearUsedImg)
            {

                if (partForWorkOrder.UsedImageFile != null)
                {
                    var usedBytes = await FileURL.GetFileBytes(partForWorkOrder.UsedImageFile);
                    if (usedBytes != partForWorkOrder.UsedImageBytes)
                    {
                        partForWorkOrder.UsedImageBytes = usedBytes;
                    }
                }
            }
            else
            {
                partForWorkOrder.UsedImageBytes = null;
            }
            return partForWorkOrder;
        }


        private async Task<PartForWorkOrder> SetImageUrls(PartForWorkOrder partForWorkOrder)
        {
            if(partForWorkOrder.PartForWorkImgId != null)
            {
                partForWorkOrder.PartForWorkImg = await _repo.GetPartForWorkImg((Guid)partForWorkOrder.PartForWorkImgId);
            }
            if (partForWorkOrder.MachiningImageBytes != null)
            {
                partForWorkOrder.MachiningImageUrl = FileURL.GetImageURL(partForWorkOrder.MachiningImageBytes);
            }
            if (partForWorkOrder.CladdingImageBytes != null)
            {
                partForWorkOrder.CladdingImageUrl = FileURL.GetImageURL(partForWorkOrder.CladdingImageBytes);
            }
            if (partForWorkOrder.FinishedImageBytes != null)
            {
                partForWorkOrder.FinishedImageUrl = FileURL.GetImageURL(partForWorkOrder.FinishedImageBytes);
            }
            if (partForWorkOrder.UsedImageBytes != null)
            {
                partForWorkOrder.UsedImageUrl = FileURL.GetImageURL(partForWorkOrder.UsedImageBytes);
            }
            if (partForWorkOrder.PartForWorkImg != null && partForWorkOrder.PartForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkOrder.PartForWorkImg);
            return partForWorkOrder;
        }
    }
}
