using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class EditModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Letters or numbers")]
        public String UserName { get; set; }
        public String ProfileImage { get; set; }
        public String PageUrl { get; set; }
        public String About { get; set; }
    }
}