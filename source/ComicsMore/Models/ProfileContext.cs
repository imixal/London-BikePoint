using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(): base("IdentityDb") { }

        public static ProfileContext Create()
        {
            return new ProfileContext();
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}