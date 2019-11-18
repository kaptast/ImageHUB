using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageHUB.Repositories;

namespace ImageHUB.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IImageService imageService;
        public ProfileService(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public Profile GetProfileByID(DatabaseContext context, string id, string userName)
        {
            var profile = context.GetProfileByID(id);
            
            if (profile != null)
            {
                profile.Posts = this.imageService.GetImageUrlsById(context, profile.ID);
                profile.Friends = context.GetFriends(profile.ID);
            }
            else
            {
                profile = new Profile()
                {
                    ID = id,
                    UserName = userName,
                    Posts = new List<Post>()
                };

                context.AddNewProfile(profile);
            }

            return profile;
        }

        public IEnumerable<Profile> GetAll(DatabaseContext context)
        {
            return context.GetProfiles();
        }

        public IEnumerable<Profile> GetAllByName(DatabaseContext context, string name)
        {
            return context.GetProfilesByName(name);
        }

        public void AddFriend(DatabaseContext context, string userID, string friendID)
        {
            if (this.IsFriendsWith(context, userID, friendID) == FriendStatus.NotFriends)
            {
                context.AddFriend(userID, friendID);
            }
        }

        public void AcceptFriend(DatabaseContext context, string userID, string friendID)
        {
            var friendShip = context.GetFriendShip(userID, friendID);

            if (friendShip == null)
            {
                friendShip = context.GetFriendShip(friendID, userID);
            }

            if (friendShip == null) return;

            friendShip.Accepted = true;
            context.Update(friendShip);
            context.SaveChanges();
        }

        public FriendStatus IsFriendsWith(DatabaseContext context, string userID, string friendID)
        {
            var friendShip = context.GetFriendShip(userID, friendID);
            
            if (friendShip != null)
            {
                return friendShip.Accepted ? FriendStatus.Friends : FriendStatus.Pending;
            }
            else
            {
                friendShip = context.GetFriendShip(friendID, userID);

                if (friendShip != null)
                {
                    return friendShip.Accepted ? FriendStatus.Friends : FriendStatus.Waiting;
                }
            }

            return FriendStatus.NotFriends;
        }
    }
}
