using blog.Domain;

namespace blog.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}