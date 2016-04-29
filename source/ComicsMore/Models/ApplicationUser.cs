using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 using Microsoft.AspNet.Identity.EntityFramework;

namespace ComicsMore.Models
{ 

    public class ApplicationUser : IdentityUser
    {
        public String ProfileImage { get; set; }
        public String PageUrl { get; set; }
        public String About { get; set; }

        public ApplicationUser()
        {
        }
    }
}