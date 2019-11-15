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
            return await this.Posts.Include("Owner").OrderByDescending(p => p.ID).ToListAsync();
        }

        public IEnumerable<Post> GetPostByUserID(string id)
        {
            return this.Posts.Include("Owner").Where(p => p.Owner.ID.Equals(id))?.OrderByDescending(p => p.ID).ToList();
        }

        public void SaveImage(string path, Profile owner)
        {
            this.Posts.Add(new Post(){
                Image = path,
                Owner = owner
            });

            this.SaveChanges();
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            return await this.Profiles.ToListAsync();
        }

        public IEnumerable<Profile> GetFriendsByID(string id)
        {
            return this.Profiles.AsEnumerable();
        }

        public Profile GetProfileByID(string id)
        {
            return this.Profiles.Where(p => p.ID.Equals(id)).SingleOrDefault();
        }

        public void AddNewProfile(Profile profile)
        {
            this.Profiles.Add(profile);

            this.SaveChanges();
        }

        public IEnumerable<Profile> GetProfilesByName(string name)
        {
            return this.Profiles.Where(x => x.UserName.ToLower().Contains(name.ToLower())).ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=imgHub.db");
    }
}
