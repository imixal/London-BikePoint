﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ComicsMore.Filters;
using ComicsMore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Controllers
{
    interface IObservable
    {
        void Notify();
    }

    interface IObserver
    {
        void Update();
    }



    [Culture]
    public class ProfileController : Controller, IObservable
    {
        List<IObserver> observers;
        public static String pageName;

        private Cloudinary cloud;

        public ProfileController()
        {
            cloud = new Cloudinary(new Account("comics-cloudinary", "449254596371885", "Z_gisL814YSSnRkS_x2v8W4LqCM"));
            observers = new List<IObserver>();
            observers.Add(new PublicWallMedal(this));
            observers.Add(new AuthorMedal(this));
        }

        private IdentityContext DbContext
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IdentityContext>();
            }
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [HttpPost]
        [Route("DeleteComment")]
        public JsonResult DeleteComment(int commentId)
        {
            ApplicationUser user = UserManager.FindByName(pageName);
            Comment comment = DbContext.Comments.First(c => c.Id == commentId);

            DbContext.Comments.Remove(comment);
            DbContext.SaveChanges();
            return Json(commentId, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserProfile(String name)
        {
            if (name == null)
                name = User.Identity.GetUserName();
            pageName = name;

            ApplicationUser user = UserManager.FindByName(name);
            if (user != null)
            {
                UserViewModel model = new UserViewModel
                {
                    Profile = user,
                    Comments = DbContext.Comments.Where(c => c.UserPage.Id == user.Id).ToList(),
                    Medals = user.Medals
                };

                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public void AddComment(String name, String commentBody)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            String returnUrl = Request.UrlReferrer.AbsolutePath;

            if (user != null)
            {
                if (commentBody != null)
                {
                    Comment comment = new Comment()
                    {
                        Body = commentBody,
                        Author = user,
                        UserPage = UserManager.FindByName(pageName)
                    };
                    user.Comments.Add(comment);

                    UserManager.Update(user);
                    //UpdateMedals();
                    Notify();
                    
                }
            }
        }

        public void UpdateMedals()
        {
            ApplicationUser user = UserManager.FindByName(pageName);

            if (user.Comments.Count == 10)
            {
                Medal medal = DbContext.Medals.First(m => m.Id == 7);
                user.Medals.Add(medal);
                UserManager.Update(user);
            }
        }

        public async Task<ActionResult> EditProfile(String name)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(name);
            if (user != null)
            {
                EditViewModel model = new EditViewModel { About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public JsonResult UpdateImage(String data)
        {
            HttpPostedFileBase pic = null;
            if (HttpContext.Request.Files.AllKeys.Any())
            {
                pic = HttpContext.Request.Files["HelpSectionImages"];
            }
            return Json(pic, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(EditViewModel model, HttpPostedFileBase file, String name)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(name);

            if (user != null)
            {
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
                    user.ProfileImage = uploadResult.SecureUri.ToString();
                }

                user.About = model.About;
                user.UserName = model.UserName;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserProfile", "Profile", new { name = user.UserName });
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update();
            }
        }

        public abstract class AbstractMedal
        {
           public abstract bool ConditionIsDone();
        }

        public class PublicWallMedal : AbstractMedal, IObserver
        {
            ApplicationUser user;
            ProfileController controller;

            public PublicWallMedal(ProfileController controller)
            {
                this.controller = controller;
            }

            void IObserver.Update()
            {
                user = controller.UserManager.FindByName(pageName);

                if (ConditionIsDone())
                {
                    Medal medal = controller.DbContext.Medals.First(m => m.Name == "Public wall");
                    user.Medals.Add(medal);
                    controller.UserManager.Update(user);
                }
            }

            public override bool ConditionIsDone()
            {
                if (user.Comments.Count == 10)
                    return true;
                else
                    return false;
            }
        }

        public class AuthorMedal : AbstractMedal, IObserver
        {
            ApplicationUser user;
            ProfileController controller;

            public AuthorMedal(ProfileController controller)
            {
                this.controller = controller;
            }

            void IObserver.Update()
            {
                user = controller.UserManager.FindByName(pageName);

                if (ConditionIsDone())
                {
                    Medal medal = controller.DbContext.Medals.First(m => m.Name == "Author");
                    user.Medals.Add(medal);
                    controller.UserManager.Update(user);
                }
            }

            public override bool ConditionIsDone()
            {
                if (controller.DbContext.Comments.Where(c => c.Author.Id == user.Id).Count() == 10)
                    return true;
                else
                    return false;
            }
        }
    }
}