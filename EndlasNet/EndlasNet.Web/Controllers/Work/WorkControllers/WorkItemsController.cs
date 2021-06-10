using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndlasNet.Data;
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

        public IActionResult Initialize(Guid workId)
        {
            return View();
        }

    }
}
