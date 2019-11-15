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

        public IEnumerable<Profile> GetFriends(string userID)
        {
            var result = this.Profiles.Include(p => p.FriendsWith).ThenInclude(p => p.Friend);
            return result.Where(p => p.ID.Equals(userID));
        }

        public void AddFriend(string userID, string friendID)
        {
            var friendProfile = this.GetProfileByID(friendID);

            var user = this.GetProfileByID(userID);
            user.FriendsWith.Add(new ProfileFriend { Profile = user, Friend = friendProfile, Accepted = false });

            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=imgHub.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileFriend>().HasKey(bc => new { bc.ProfileID, bc.FriendID });
            modelBuilder.Entity<ProfileFriend>().HasOne(bc => bc.Profile).WithMany(b => b.FriendsTo).HasForeignKey(bc => bc.ProfileID);
            modelBuilder.Entity<ProfileFriend>().HasOne(bc => bc.Friend).WithMany(c => c.FriendsWith).HasForeignKey(bc => bc.FriendID);
        }
    }
}
