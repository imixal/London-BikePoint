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

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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

        public async Task<ActionResult> EditUser(String name)
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
        public async Task<ActionResult> EditUser(EditViewModel model, HttpPostedFileBase file, String name)
        {
            ApplicationUser user = await UserManager.FindByNameAsync(name);

            if (user != null)
            {
                //var uploadParams = new ImageUploadParams
                //{
                //    File = new FileDescription(file)
                //};
                //var uploadResult = cloud.Upload(uploadParams);

                user.About = model.About;
                user.UserName = model.UserName;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers", "Admin", new { name = user.UserName });
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