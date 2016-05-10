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
                    //AddComment(comment);
                    comment.Id = 1;
                    comment.Author = user;
                    comment.UserPage = UserManager.FindByName(pageId);

                    UserManager.Update(user);
                }


                RedirectToAction("UserProfile", "Profile");
            }
            return RedirectToAction("Login", "Account");
        }

        public void AddComment(Comment comment)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            comment.Author = user;
            comment.Time = DateTime.UtcNow;
            user.Comments.Add(comment);
        }

        [HttpGet]
        public ActionResult Delete()
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