using blog.Domain;


namespace blog.Repositories
{
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(CrossBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}