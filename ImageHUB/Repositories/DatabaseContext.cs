using ImageHUB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        private object lockObject = new object();

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            lock (lockObject)
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Profile>().HasIndex(u => u.UserID).IsUnique();

            builder.Entity<ProfileFriend>().HasKey(bc => new { bc.ProfileID, bc.FriendID });
            builder.Entity<ProfileFriend>().HasOne(bc => bc.Profile).WithMany(b => b.FriendsTo).HasForeignKey(bc => bc.ProfileID);
            builder.Entity<ProfileFriend>().HasOne(bc => bc.Friend).WithMany(c => c.FriendsWith).HasForeignKey(bc => bc.FriendID);

            builder.Entity<PostTag>().HasIndex(pt => new { pt.PostID, pt.TagID });
            builder.Entity<PostTag>().HasOne(pt => pt.Tag).WithMany(t => t.Posts).HasForeignKey(pt => pt.TagID);
            builder.Entity<PostTag>().HasOne(pt => pt.Post).WithMany(p => p.Tags).HasForeignKey(pt => pt.PostID);
        }
    }
}
