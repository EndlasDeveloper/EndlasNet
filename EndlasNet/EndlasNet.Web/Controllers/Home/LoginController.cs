﻿using Microsoft.AspNetCore.Http;
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
            ViewBag.IsUserLoggedIn = "true";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("firstname");
            HttpContext.Session.Remove("lastname");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("authstr");
            return View("../Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginSubmitAsync(string email, string pwd)
        {
            email += "@endlas.com";
            var user = await _db.GetUser(email.ToLower());

            //Specify on next view what the error was using ViewBag to send message
            if (user == null || Utility.Security.ComputeSha256Hash(pwd) != user.AuthString)
            {
                // login failed
                ViewBag.IsUserLoggedIn = "false";
                return View("../Login/Index");
            }
            // login success
            ViewBag.IsUserLoggedIn = "true";

            // set privilege of user to appropriate level for views
            ViewBag.UserPrivilege = _db.GetUserPrivileges(user); // either "Admin" or "Employee"
          
            // set user credentials for session
            HttpContext.Session.SetString("userId", user.UserId.ToString());
            HttpContext.Session.SetString("firstname", user.FirstName);
            HttpContext.Session.SetString("lastname", user.LastName);
            HttpContext.Session.SetString("email", user.EndlasEmail);
            HttpContext.Session.SetString("authstr", user.AuthString);

            return View("../Home/Index");
        }
        [HttpGet]
        public Task<bool> LoggedIn()
        {
            var myUser = HttpContext.User;
            return Task.FromResult(myUser.Identities.Any(x => x.IsAuthenticated));
        }
    }
}
