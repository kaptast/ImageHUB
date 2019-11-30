using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageHUB.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DatabaseContext database;

        public ProfileRepository(DatabaseContext db)
        {
            this.database = db;
        }

        public void Add(Profile entity)
        {
            try
            {
                this.database.Profiles.Add(entity);
                this.database.SaveChanges();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void Delete(Profile entity)
        {
            this.database.Remove(entity);
            this.database.SaveChanges();
        }

        public IEnumerable<Profile> GetAll()
        {
            return this.database.Profiles.ToList();
        }

        public Profile GetByID(string id)
        {
            return this.database.Profiles.Where(p => p.UserID.Equals(id)).Include(ft => ft.FriendsTo).ThenInclude(ft => ft.Friend).Include(fw => fw.FriendsWith).ThenInclude(fw => fw.Profile).SingleOrDefault();
        }

        public IEnumerable<Profile> GetFriends(string userID, bool selectPending = false)
        {
            var user = this.GetByID(userID);
            var friendsTo = user.FriendsTo?.Where(p => p.Accepted || selectPending).Select(x => x.Friend)?.ToList();
            var friendsWith = user.FriendsWith?.Where(p => p.Accepted || selectPending).Select(x => x.Profile)?.ToList();
            var friends = friendsTo.Concat(friendsWith);
            return friends;
        }

        public ProfileFriend GetFriendShip(string userID, string friendID)
        {
            var user = this.GetByID(userID);
            var friend = this.GetByID(friendID);

            return user.FriendsTo?.Where(pf => pf.ProfileID.Equals(user.ID) && pf.FriendID.Equals(friend.ID)).SingleOrDefault();
        }

        public IEnumerable<Profile> GetProfilesByName(string userName)
        {
            return this.database.Profiles.Where(p => p.UserName.ToLower().Contains(userName.ToLower())).ToList();
        }

        public void Update(Profile entity)
        {
            this.database.Profiles.Update(entity);
            this.database.SaveChanges();
        }

        public void UpdateFriendShip(ProfileFriend friendship)
        {
            this.database.Update(friendship);
            this.database.SaveChanges();
        }
    }
}
