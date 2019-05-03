using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.Domain
{
    public class ArticleTag :BaseEntity
    {        
        [Required]
        public virtual Tag Tag { get; set; }    

        [Required]
         public virtual Article Article { get; set; }
        
    }
}