﻿using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Mappings
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(
            EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "public");

            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(64);

            builder.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(256);

            builder.Property(e => e.IsDeleted)
               .HasColumnName("is_deleted")
               .HasDefaultValue(false);

            builder.Property(e => e.Role)
                .HasColumnName("role")
                .HasConversion<string>()
                .HasDefaultValue(Role.User);

            builder.HasMany(e => e.Topics)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId);

            builder.HasMany(e => e.Comments)
                .WithOne(e => e.Creator)
                .HasForeignKey(e => e.CreatorId);

            builder.HasIndex(e => e.Username)
                .HasDatabaseName("UX_public_users_username")            
                .IsUnique();

            builder.HasIndex(e => e.Email)
                .HasDatabaseName("UX_public_users_email")
                .IsUnique();

            builder.HasIndex(e => new { e.Username, e.Role })
                .HasDatabaseName("IX_public_users_usename_role");
        }
    }
}
