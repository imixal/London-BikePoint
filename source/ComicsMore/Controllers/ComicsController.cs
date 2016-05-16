using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Controllers
{
    public class ComicsController : Controller
    {
        Cloudinary cloud;

        public ComicsController()
        {
            cloud = new Cloudinary(new Account("comics-cloudinary", "449254596371885", "Z_gisL814YSSnRkS_x2v8W4LqCM"));
        }

        public ActionResult CreateComicStrip()
        {
            ViewBag.Message = "Comics More Editor";

            return View();
        }

        public ActionResult PublishComicStrips()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult UploadImage(String data)
        {
            HttpPostedFileBase file = null;
            String imageUrl = null;

            if (HttpContext.Request.Files.AllKeys.Any())
            {
                file = HttpContext.Request.Files["HelpSectionImages"];
            }

            if (file != null)
            {
                String fileName = file.FileName;
                String path = Server.MapPath("~/Images/") + fileName;

                file.SaveAs(path);


                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(path)
                };

                var uploadResult = cloud.Upload(uploadParams);
                imageUrl = uploadResult.SecureUri.ToString();
            }

            return Json(imageUrl, JsonRequestBehavior.AllowGet);
        }
    }
}