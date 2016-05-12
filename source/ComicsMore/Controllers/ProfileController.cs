using ComicsMore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Controllers
{
    public class ProfileController : Controller
    {
        private IdentityContext dbContext { get; set; }
        public static String pageId;


        public ProfileController()
        {
            dbContext = new IdentityContext();
        }



        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [HttpGet]
        public ActionResult UserProfile(String id)
        {
            if (id == null)
                id = User.Identity.GetUserName();
            pageId = id;
            // TODO: Use pageId
            ApplicationUser user = UserManager.FindByName(id);
            if (user != null)
            {
                //ApplicationUser model = new ApplicationUser
                //{ About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName, Id = user.Id }
                UserViewModel model = new UserViewModel
                {
                    Profile = user,
                    Comments = dbContext.Comments.Where(c => c.UserPage.Id == user.Id).ToList()
                };

                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult UserProfile(String id, Comment comment)
        {
            ApplicationUser user = UserManager.FindByName(User.Identity.Name);
            String returnUrl = Request.UrlReferrer.AbsolutePath;

            if (user != null)
            {
                //ApplicationUser model = new ApplicationUser
                //{ About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName, Id = user.Id };
                //UserViewModel model = new UserViewModel
                //{
                //    Profile = user,
                //    Comments = dbContext.Comments.Where(c => c.UserPage.Id == pageId).ToList()
                //};

                if (comment.Body != null)
                {
                    comment.Author = user;
                    comment.UserPage = UserManager.FindByName(pageId);
                    user.Comments.Add(comment);

                    UserManager.Update(user);
                }

                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Account");
        }
        
        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            ApplicationUser user = UserManager.FindByName(pageId);
            Comment comment = dbContext.Comments.Where(c => c.Id == commentId).First();
            var com = user.Comments.Where(c => c.Id == commentId).First();

            user.Comments.Remove(com);
            UserManager.Update(user);
            return RedirectToAction("UserProfile", "Profile");
        }

        [HttpGet]
        public ActionResult DeleteProfile()
        {
            return PartialView();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> EditProfile(String id)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(id);
            if (user != null)
            {
                EditViewModel model = new EditViewModel { About = user.About, ProfileImage = user.ProfileImage, UserName = user.UserName };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}