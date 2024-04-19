using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Likes)
                .IsRequired();

            builder.Property(c => c.Text)
                .HasMaxLength(500); 

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(c => c.Creator)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.SetNull); 

            builder.HasOne(c => c.Topic)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TopicId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}