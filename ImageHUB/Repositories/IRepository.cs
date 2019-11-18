using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Repositories
{
    public interface IRepository
    {
        IEnumerable<Post> GetAllPosts(string userID);
        IEnumerable<Post> GetPostByUserID(string id);
        void SaveImage(string path, Profile owner);
        IEnumerable<Profile> GetProfiles();
        Profile GetProfileByID(string id);
        void AddNewProfile(Profile profile);
        IEnumerable<Profile> GetProfilesByName(string name);
        IEnumerable<Profile> GetFriends(string userID, bool selectPending = false);
        ProfileFriend GetFriendShip(string userID, string friendID);
        void AddFriend(string userID, string friendID);
        void UpdateFriendShip(ProfileFriend friendShip);
    }
}
