using ImageHUB.Entities;
using System.Collections.Generic;

namespace ImageHUB.Services
{
    public interface IProfileService
    {
        Profile GetProfileByID(string userID, string userName);
        IEnumerable<Profile> GetProfilesByName(string userName);
        void AddFriend(string userID, string friendID);
        void AcceptFriend(string userID, string friendID);
        void DeleteFriend(string userID, string friendID);
        FriendStatus IsFriendsWith(string userID, string friendID);
        IEnumerable<Profile> GetWaitingFriends(string userID);
    }
}
