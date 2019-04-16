using System;

namespace blog.Domain
{
    public class ArticleTag :BaseEntity
    {
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }    

        public int IdArticle { get; set; }  
 
         public virtual Article Article { get; set; }

        
    }
}