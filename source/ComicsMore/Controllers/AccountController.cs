using ComicsMore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ComicsMore.Controllers
{
    public class AccountController : Controller
    {
        Cloudinary cloud;


        public AccountController()
        {
            cloud = new Cloudinary(new Account("duke-cloudinary", "449254596371885", "Z_gisL814YSSnRkS_x2v8W4LqCM"));
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Nickname, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Nickname, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        //[HttpGet]
        //public ActionResult Delete()
        //{
        //    return PartialView();
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public async Task<ActionResult> DeleteConfirmed()
        //{
        //    ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        IdentityResult result = await UserManager.DeleteAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Logout", "Account");
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //public async Task<ActionResult> EditProfile(String id)
        //{
        //    ApplicationUser user = await UserManager.FindByNameAsync(id);
        //    if (user != null)
        //    {
        //        EditViewModel model = new EditViewModel { About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName };
        //        return View(model);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        [HttpPost]
        public async Task<ActionResult> EditProfile(EditViewModel model, HttpPostedFileBase file, String id)
        {
            ApplicationUser userProfile = await UserManager.FindByNameAsync(id);
            if (userProfile != null)
            {
                if (file != null)
                {
                    // TODO: cloud upload.
                    var path = Server.MapPath("~/Content/" + file.FileName);
                    file.SaveAs(path);
                }

                userProfile.About = model.About;
                userProfile.UserName = model.UserName;

                IdentityResult result = await UserManager.UpdateAsync(userProfile);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserProfile", "Account", new { id = userProfile.UserName });
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

        [HttpGet]
        public ActionResult UserProfile(String id, Comment comment)
        {
            if (id == null)
                id = User.Identity.GetUserName();
            ApplicationUser user = UserManager.FindByName(id);
            if (user != null)
            {
                //ApplicationUser model = new ApplicationUser
                //{ About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName, Id = user.Id };
                using (ProfileContext db = new ProfileContext())
                {

                    UserViewModel model = new UserViewModel
                    {
                        Profile = user,
                        Comments = db.Comments
                    };

                    return View(model);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult UserProfile(Comment comment)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            if (user != null)
            {
                //ApplicationUser model = new ApplicationUser
                //{ About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName, Id = user.Id };
                using (ProfileContext db = new ProfileContext())
                {

                    UserViewModel model = new UserViewModel
                    {
                        Profile = user,
                        Comments = db.Comments
                    };

                    if (comment.Body != null)
                    {
                        //AddComment(comment);
                        model.Profile.Comments.Add(comment);
                    }

                    return View(model);
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public void AddComment(Comment comment)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
           // comment.Author = user;
            comment.Time = DateTime.UtcNow;
            user.Comments.Add(comment);
        }
    }
}