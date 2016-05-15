using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Controllers
{
    public class ComicsController : Controller
    {

        public ActionResult CreateComicStrip()
        {
            ViewBag.Message = "Comics More Editor";

            return View();
        }

        public ActionResult PublishComicStrips()
        {
            return PartialView();
        }
    }
}