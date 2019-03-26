using blog.Domain;

namespace blog.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}