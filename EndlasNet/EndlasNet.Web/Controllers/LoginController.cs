using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EndlasNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndlasNet.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserRepo _db;

        public LoginController(UserRepo db)
        {
            _db = db;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.UserLoginStatus = "success";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return View("../Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginSubmitAsync(string email, string pwd)
        {
            var user = await _db.GetUser(email);

            //Specify on next view what the error was using ViewBag to send message
            if (user == null || ShaHash.ComputeSha256Hash(pwd) != user.AuthString)
            {
                ViewBag.UserLoginStatus = "failed";
                return View("../Login/Index");
            }

            ViewBag.UserLoginStatus = "success";

            HttpContext.Session.SetString("username", user.FirstName);
            HttpContext.Session.SetString("userId", user.UserId.ToString());

            return View("../Home/Index");
        }
        [HttpGet]
        public async Task<bool> LoggedIn()
        {
            var myUser = HttpContext.User;
            return myUser.Identities.Any(x => x.IsAuthenticated);
        }
    }
}
