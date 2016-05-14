using ComicsMore.Filters;
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
    [Culture]
    public class ProfileController : Controller
    {
        public static String pageId;
        
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
                UserViewModel model = new UserViewModel
                {
                    Profile = user,
                    //Comments = dbContext.Comments.Where(c => c.UserPage.Id == user.Id).ToList(),
                    Comments = user.Comments,
                    Medals = user.Medals
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
                if (comment.Body != null)
                {
                    comment.Author = user;
                    comment.UserPage = UserManager.FindByName(pageId);
                    user.Comments.Add(comment);

                    UserManager.Update(user);
                    UpdateMedals();
                }

                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Account");
        }

        public void UpdateMedals()
        {
            ApplicationUser user = UserManager.FindByName(pageId);

            if(user.Comments.Count >= 10)
            {
                Medal medal = DbContext.Medals.First(m => m.Id == 7);
                user.Medals.Add(medal);
                UserManager.Update(user);
            }
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            ApplicationUser user = UserManager.FindByName(pageId);
            Comment comment = DbContext.Comments.First(c => c.Id == commentId);

            user.Comments.Remove(comment);
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
                    return RedirectToAction("UserProfile", "Profile", new { id = userProfile.UserName });
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
    }
}