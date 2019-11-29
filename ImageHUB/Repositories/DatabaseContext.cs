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

        private object lockObject = new object();

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            lock(lockObject){
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Profile>().HasIndex(u => u.UserID).IsUnique();
        }
    }
}
