using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class UserViewModel
    {
        public ApplicationUser Profile { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}