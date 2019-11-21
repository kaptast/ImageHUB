using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using Microsoft.Extensions.Logging;

namespace ImageHUB.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IImageService imageService;
        private readonly IRepository repository;
        private readonly ILogger<Startup> logger;
        public ProfileService(IImageService imageService, IRepository repo, ILogger<Startup> logger)
        {
            this.imageService = imageService;
            this.repository = repo;
            this.logger = logger;
        }

        public Profile GetProfileByID(string id, string userName)
        {
            var profile = repository.GetProfileByID(id);
            
            if (profile != null)
            {
                profile.Posts = this.imageService.GetImageUrlsById(profile.ID);
                profile.Friends = repository.GetFriends(profile.ID);
            }
            else
            {
                profile = new Profile()
                {
                    ID = id,
                    UserName = userName,
                    Posts = new List<Post>()
                };

                repository.AddNewProfile(profile);
            }

            return profile;
        }

        public IEnumerable<Profile> GetAll()
        {
            return repository.GetProfiles();
        }

        public IEnumerable<Profile> GetAllByName(string name)
        {
            return repository.GetProfilesByName(name);
        }

        public void AddFriend(string userID, string friendID)
        {
            if (this.IsFriendsWith(userID, friendID) == FriendStatus.NotFriends)
            {
                repository.AddFriend(userID, friendID);
            }
        }

        public void AcceptFriend(string userID, string friendID)
        {
            var friendShip = repository.GetFriendShip(userID, friendID);

            if (friendShip == null)
            {
                friendShip = repository.GetFriendShip(friendID, userID);
            }

            if (friendShip == null) return;

            friendShip.Accepted = true;
            repository.UpdateFriendShip(friendShip);
        }

        public FriendStatus IsFriendsWith(string userID, string friendID)
        {
            var friendShip = repository.GetFriendShip(userID, friendID);
            
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

        public void AddProfile(string userID, string userName)
        {
            var profile = new Profile()
            {
                ID = userID,
                UserName = userName,
                Avatar = userID
            };
            this.repository.AddNewProfile(profile);
        }
    }
}
