using System.ComponentModel.DataAnnotations;

namespace blog.Domain
{
    public class CategoryArticle : BaseEntity
    {        
        [Required]
        public Category Category { get; set; }        

        [Required]
        public Article Article { get; set; }
        
    }
}