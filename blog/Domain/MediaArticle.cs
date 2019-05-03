using System;
using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class MediaArticle : BaseEntity
    {
        [Required]
        public Article Article { get; set; }
        [Required]
        public Media Media { get; set; }
    }
}