using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext() : 
            base("IdentityDb") { }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Medal> Medals { get; set; }

    }

    public class ApplicationUser : IdentityUser
    {
        public String ProfileImage { get; set; }
        public String About { get; set; }
        public String Style { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Medal> Medals { get; set; }
        public virtual ICollection<ComicStrip> Comics { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public ApplicationUser()
        {
            Style = "~/Content/css";
        }
    }
}