using blog.Domain;

namespace blog.Repositories
{
    public class MediaArticleRepository :GenericRepository<MediaArticle>, IMediaArticleRespository
    {

        public MediaArticleRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}