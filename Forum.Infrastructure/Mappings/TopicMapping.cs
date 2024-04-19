using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Mappings
{
    public class TopicMapping : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topics");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Subject)
                .HasMaxLength(500);

            builder.Property(t => t.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.Likes)
                .IsRequired();

            builder.HasOne(t => t.Creator)
                .WithMany(u => u.Topics)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.Comments)
                .WithOne(c => c.Topic)
                .HasForeignKey(c => c.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}