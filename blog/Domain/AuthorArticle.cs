using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class AuthorArticle
    {
        [Key]
        public int Id { get; set; }
        public int  IdArticle { get; set; }

        public Article Article { get; set; }
        public int IdAuthor { get; set; }

        public Author Author { get; set; }

    }
}