using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicsMore.Models
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Letters or numbers")]
        public String Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [HiddenInput]
        public String ReturnUrl { get; set; }
    }
}