using Forum.Domain.Models;
using Forum.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DatabaseContext(DbContextOptions options) 
            : base(options)
        {
        
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new CommentMapping());
            modelBuilder.ApplyConfiguration(new TopicMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
