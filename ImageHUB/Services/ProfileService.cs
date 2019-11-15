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
            return context.GetProfiles().Result;
        }

        public IEnumerable<Profile> GetAllByName(DatabaseContext context, string name)
        {
            return context.GetProfilesByName(name);
        }

        public void AddFriend(DatabaseContext context, string userID, string friendID)
        {
            if (!this.IsFriendsWith(context, userID, friendID))
            {
                context.AddFriend(userID, friendID);
            }
        }

        public bool IsFriendsWith(DatabaseContext context, string userID, string friendID)
        {
            return context.GetFriends(userID).Where(f => f.ID.Equals(friendID)).Count() > 0;
        }
    }
}
