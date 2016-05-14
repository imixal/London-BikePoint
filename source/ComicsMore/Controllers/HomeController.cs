using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicsMore.Models;
using ComicsMore.Filters;
using System.Web.Optimization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ComicsMore.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult UserList()
        {
            IdentityContext db = IdentityContext.Create();

            return View(UserManager.Users);
        }

        public ActionResult ChangeCulture(String lang)
        {
            String returnUrl = Request.UrlReferrer.AbsolutePath;
            List<String> cultures = new List<String>() { "en", "ru" };

            if (!cultures.Contains(lang))
            {
                lang = "en";
            }

            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddMonths(1);
            }

            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public ActionResult ChangeStyle(String style)
        {
            String returnUrl = Request.UrlReferrer.AbsolutePath;
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());

            if (style.ToLower() == "dark")
                user.Style = "dark";
            else
                user.Style = "light";

            UserManager.Update(user);

            return Redirect(returnUrl);
        }
    }
}