namespace blog.Domain
{
    public class CategoryArticle : BaseEntity
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int IdArticle { get; set; }

        public Article Article { get; set; }
        
    }
}