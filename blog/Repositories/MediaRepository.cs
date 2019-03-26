using blog.Domain;

namespace blog.Repositories
{
    public class MediaRepository : GenericRepository<Media>, IMediaRepository
    {        
        public MediaRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
    }
}