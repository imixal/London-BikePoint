using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public virtual ApplicationUser Author { get; set; }
        [Required]
        public virtual ApplicationUser UserPage { get; set; }
        [Required]
        public String Body { get; set; }

        public Comment(ApplicationUser author, String body)
        {
            Time = DateTime.Now;
            Author = author;
            Body = body;
        }

        public Comment()
        {
            Time = DateTime.Now;
        }
    }
}