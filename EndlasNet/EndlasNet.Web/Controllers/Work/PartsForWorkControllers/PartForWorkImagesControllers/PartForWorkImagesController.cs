using EndlasNet.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Controllers
{
    public class PartForWorkImagesController : Controller
    {
        private IPartForJobRepo _repo;

        public PartForWorkImagesController(IPartForJobRepo repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var partImages = await _repo.GetAllPartForWorkImgs();
            foreach (PartForWorkImg img in partImages)
            {
                img.ImageUrl = FileURL.GetImageURL(img.ImageBytes);
            }
            return View(await _repo.GetAllPartForWorkImgs());
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageName,ImageFile")] PartForWorkImg partForWorkImg)
        {
            if (ModelState.IsValid)
            {
                partForWorkImg.PartForWorkImgId = Guid.NewGuid();
                if (partForWorkImg.ImageFile != null)
                {
                    partForWorkImg.ImageBytes = await FileURL.GetFileBytes(partForWorkImg.ImageFile);
                }
                await _repo.AddPartForWorkImg(partForWorkImg);
                return RedirectToAction("Index");
            }
            return View(partForWorkImg);
        }

        // GET: PartsForAJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

                var partForWorkImg = await _repo.GetPartForWorkImg((Guid)id);
                FileURL.SetImageURL(partForWorkImg);
                
            
            if (partForWorkImg == null)
            {
                return NotFound();
            }
            if (partForWorkImg.ImageBytes != null)
                FileURL.SetImageURL(partForWorkImg);

            return View(partForWorkImg);
        }

        // POST: PartsForAJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var partForWorkId = await _repo.GetPartForWorkImg(id);
            await _repo.DeletePartForWorkImg(partForWorkId);
            return RedirectToAction("Index");
        }
    }
}
