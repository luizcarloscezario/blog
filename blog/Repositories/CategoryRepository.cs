using blog.Domain;

namespace blog.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }        
        
    }                                                                                                                                                                                                                                                                                            
}