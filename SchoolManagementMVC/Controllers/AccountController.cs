using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace SchoolManagementMVC.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager)
        {
            userManager = _userManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}