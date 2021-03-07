using EndlasNet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using EndlasNet.Data;
using Microsoft.AspNetCore.Http;
using System;

namespace EndlasNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepo _db;
        public HomeController(UserRepo db)
        {
            _db = db;
        }

        public IActionResult Index()
        {      
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Work()
        {
            return View();
        }

        public IActionResult Inventory()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Environments()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Issues()
        {
            return View();
        }

        public IActionResult ViewERD()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
