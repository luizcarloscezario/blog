using blog.Domain;

namespace blog.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
    }
}