using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public class Repository : IRepository
    {
        private DatabaseContext context;
        public Repository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddFriend(string userID, string friendID)
        {
            this.context.AddFriend(userID, friendID);
        }

        public void AddNewProfile(Profile profile)
        {
            this.context.AddNewProfile(profile);
        }

        public IEnumerable<Post> GetAllPosts(string userID)
        {
            return this.context.GetAllPosts(userID);
        }

        public IEnumerable<Profile> GetFriends(string userID, bool selectPending = false)
        {
            return this.context.GetFriends(userID, selectPending);
        }

        public ProfileFriend GetFriendShip(string userID, string friendID)
        {
            return this.context.GetFriendShip(userID, friendID);
        }

        public IEnumerable<Post> GetPostByUserID(string id)
        {
            return this.context.GetPostByUserID(id);
        }

        public Profile GetProfileByID(string id)
        {
            return this.context.GetProfileByID(id);
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return this.context.GetProfiles();
        }

        public IEnumerable<Profile> GetProfilesByName(string name)
        {
            return this.context.GetProfilesByName(name);
        }

        public void SaveImage(string path, Profile owner)
        {
            this.context.SaveImage(path, owner);
        }

        public void UpdateFriendShip(ProfileFriend friendShip)
        {
            this.context.Update(friendShip);
            this.context.SaveChanges();
        }
    }
}
