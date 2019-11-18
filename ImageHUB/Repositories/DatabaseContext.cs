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

        public DatabaseContext()
        {
            Database.Migrate();
        }

        public async Task<IEnumerable<Post>> GetAllPosts(string userID)
        {
            var friends = this.GetFriends(userID, false);
            return await this.Posts.Include(p => p.Owner).Where(p => p.Owner.ID.Equals(userID) || friends.Contains(p.Owner)).OrderByDescending(p => p.ID).ToListAsync();
        }

        public IEnumerable<Post> GetPostByUserID(string id)
        {
            return this.Posts.Include(p => p.Owner).Where(p => p.Owner.ID.Equals(id))?.OrderByDescending(p => p.ID).ToList();
        }

        public void SaveImage(string path, Profile owner)
        {
            using (var transaction = this.Database.BeginTransaction())
            {
                this.Posts.Add(new Post()
                {
                    Image = path,
                    Owner = owner
                });

                this.SaveChanges();
                transaction.Commit();
            }
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            return await this.Profiles.ToListAsync();
        }

        public Profile GetProfileByID(string id)
        {
            return this.Profiles.Where(p => p.ID.Equals(id)).Include(ft => ft.FriendsTo).ThenInclude(ft => ft.Friend).Include(fw => fw.FriendsWith).ThenInclude(fw => fw.Profile).SingleOrDefault();
        }

        public void AddNewProfile(Profile profile)
        {
            using (var transaction = this.Database.BeginTransaction())
            {
                this.Profiles.Add(profile);
                this.SaveChanges();
                transaction.Commit();
            }
        }

        public IEnumerable<Profile> GetProfilesByName(string name)
        {
            return this.Profiles.Where(x => x.UserName.ToLower().Contains(name.ToLower())).ToList();
        }

        public IEnumerable<Profile> GetFriends(string userID, bool selectPending = false)
        {
            var user = this.GetProfileByID(userID);
            var friendsTo = user.FriendsTo?.Where(p => p.Accepted || selectPending).Select(x => x.Friend)?.ToList();
            var friendsWith = user.FriendsWith?.Where(p => p.Accepted || selectPending).Select(x => x.Profile)?.ToList();
            var friends = friendsTo.Concat(friendsWith);
            return friends;
        }

        public ProfileFriend GetFriendShip(string userID, string friendID)
        {
            var user = this.GetProfileByID(userID);
            return user.FriendsTo.Where(pf => pf.ProfileID.Equals(userID) && pf.FriendID.Equals(friendID)).SingleOrDefault();
        }

        public void AddFriend(string userID, string friendID)
        {
            using (var transaction = this.Database.BeginTransaction())
            {
                var friendProfile = this.GetProfileByID(friendID);
                var user = this.GetProfileByID(userID);
                var p2f = new ProfileFriend();
                p2f.Profile = user;
                p2f.Friend = friendProfile;
                user.FriendsTo.Add(p2f);

                this.SaveChanges();
                transaction.Commit();
            }
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
