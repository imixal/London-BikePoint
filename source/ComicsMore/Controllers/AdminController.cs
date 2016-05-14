using ComicsMore.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IdentityContext DbContext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IdentityContext>();
            }
        }

        public ActionResult ManageUsers()
        {
            return View(DbContext.Users);
        }

        public ActionResult DeleteUser()
        {
            return RedirectToAction("ManageUsers");
        }

        public ActionResult EditUser(String name)
        {

            return View(name);
        }
    }
}