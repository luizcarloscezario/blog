using blog.Domain;

namespace blog.Repositories
{
    public class AuthorRepository:GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }        
    }
}