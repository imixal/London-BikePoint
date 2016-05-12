﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicsMore.Models;
using ComicsMore.Filters;

namespace ComicsMore.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        
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

            return View(db.Users);
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
    }
}