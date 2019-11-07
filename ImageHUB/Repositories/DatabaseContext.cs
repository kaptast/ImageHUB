using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ImageHUB.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; } 

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await this.Posts.ToListAsync();
        }

        public IEnumerable<Post> GetPostByID(string id)
        {
            return this.Posts.Where(p => p.ID.Equals(id)).ToList();
        }

        public void SaveImage(string path, string id, string userName)
        {
            this.Posts.Add(new Post(){
                Image = path,
                ID = id,
                UserName = userName
            });

            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=imgHub.db");
    }
}
