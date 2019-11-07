using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImageHUB.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; } 

        public IEnumerable<Post> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Post> GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(string path, string id, string userName)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=imgHub.db");
    }
}
