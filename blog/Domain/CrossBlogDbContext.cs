using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace blog.Domain
{
    public partial class CrossBlogDbContext : DbContext
    {
        
        public DbSet<Article> Articles { get; set; }                
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorArticle> AuthorArticle { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTag { get; set; }
        public DbSet<CategoryArticle> CategoryArticle { get; set; }
        public DbSet<MediaArticle> MediaArticle { get; set; }

        




        public CrossBlogDbContext(DbContextOptions<CrossBlogDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasOne(c => c.Article).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}
