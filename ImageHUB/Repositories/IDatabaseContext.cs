using System.Collections.Generic;

namespace ImageHUB.Repositories
{
    public interface IDatabaseContext
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
        void UpdateFriendShip(ProfileFriend friendship);
    }
}
