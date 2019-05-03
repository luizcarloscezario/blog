using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Domain
{
    public class AuthorArticle : BaseEntity
    {
        [Required]
        public Article Article { get; set; }
        
        [Required]
        public Author Author { get; set; }

    }
}