using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Model
{
    public class AuthorModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Media { get; set; }

        
    }
}