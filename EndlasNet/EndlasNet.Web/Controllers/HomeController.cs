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
        private readonly ILogger<HomeController> _logger;
        public HomeController(UserRepo db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            // The HttpContext associated with the page can be accessed by the Context property.
            /*System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                sb.Append("Number of items in Session state: " +
                    Context.Session.Count.ToString() + "<br/>");
            }
            catch
            {
                sb.Append("Session state not enabled. <br/>");
            }*/
            return View();

        }

        public IActionResult Privacy()
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
