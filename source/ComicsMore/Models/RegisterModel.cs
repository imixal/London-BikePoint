using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComicsMore.Models
{
    public class RegisterModel
    {
        [Required]
        public String Nickname{ get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public String PasswordConfirm { get; set; }
    }
}