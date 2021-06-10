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
        public IActionResult Index()
        {
             
            return View();
        }

    }
}
