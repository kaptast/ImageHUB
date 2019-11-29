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

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
