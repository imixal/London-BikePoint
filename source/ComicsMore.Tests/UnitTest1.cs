using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComicsMore.Models;
using ComicsMore.Controllers;
using System.Web.Mvc;

namespace ComicsMore.Tests
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void UserProfile_NotNullExpected()
        {
            ProfileController controller = new ProfileController();

            //ViewResult result = controller.UserProfile(new Comment(new ApplicationUser(), "text")) as ViewResult;
            //UserViewModel data = result.Model as UserViewModel;

            //Assert.IsNotNull(data.Comments);
        }
    }
}
