using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComicsMore.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
    }

    public class ComicStrip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public double? Rating { get; set; }
        [Required]
        public String Json { get; set; }


        public ComicStrip()
        {
            Time = DateTime.Now;
        }
    }

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

    public class Medal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Contidion { get; set; }
        [Required]
        public String ImageUrl { get; set; }
        public virtual ICollection<ApplicationUser> Owners{ get; set; }
    }
}