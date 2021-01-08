using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Entities;
using ImageHUB.Repositories;

namespace ImageHUB.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository repository;

        public ProfileService(IProfileRepository repo)
        {
            this.repository = repo;
        }
        private Profile AddProfile(string userID, string userName)
        {
            var profile = new Profile()
            {
                UserID = userID,
                UserName = userName
            };

            this.repository.Add(profile);

            return profile;
        }

        public Profile GetProfileByID(string userID, string userName)
        {
            var profile = this.repository.GetByID(userID);

            if (profile == null)
            {
                profile = AddProfile(userID, userName);
            }

            profile.Friends = this.repository.GetFriends(userID);

            return profile;
        }

        public IEnumerable<Profile> GetProfilesByName(string userName)
        {
            var profiles = this.repository.GetProfilesByName(userName);

            return profiles;
        }

        public void AddFriend(string userID, string friendID)
        {
            if (this.IsFriendsWith(userID, friendID) == FriendStatus.NotFriends)
            {
                var friendProfile = this.repository.GetByID(friendID);
                var userProfile = this.repository.GetByID(userID);
                var p2f = new ProfileFriend();
                p2f.Profile = userProfile;
                p2f.Friend = friendProfile;
                userProfile.FriendsTo.Add(p2f);

                this.repository.Update(userProfile);
            }
        }

        public void AcceptFriend(string userID, string friendID)
        {
            var friendShip = this.repository.GetFriendShip(userID, friendID);

            if (friendShip == null)
            {
                friendShip = this.repository.GetFriendShip(friendID, userID);
            }

            if (friendShip == null) return;

            friendShip.Accepted = true;

            this.repository.UpdateFriendShip(friendShip);
        }

        public void DeleteFriend(string userID, string friendID)
        {
            var friendShip = this.repository.GetFriendShip(userID, friendID);

            if (friendShip == null)
            {
                friendShip = this.repository.GetFriendShip(friendID, userID);
            }

            if (friendShip == null) return;

            this.repository.DeleteFriendShip(friendShip);
        }

        public FriendStatus IsFriendsWith(string userID, string friendID)
        {
            var friendShip = this.repository.GetFriendShip(userID, friendID);

            if (friendShip != null)
            {
                return friendShip.Accepted ? FriendStatus.Friends : FriendStatus.Pending;
            }
            else
            {
                friendShip = repository.GetFriendShip(friendID, userID);

                if (friendShip != null)
                {
                    return friendShip.Accepted ? FriendStatus.Friends : FriendStatus.Waiting;
                }
            }

            return FriendStatus.NotFriends;
        }

        public IEnumerable<Profile> GetWaitingFriends(string userID)
        {
            return this.repository.GetWaitingFriends(userID);
        }
    }
}
