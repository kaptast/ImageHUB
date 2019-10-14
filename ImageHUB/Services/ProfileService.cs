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
        public Profile GetProfileByID(string id, string userName)
        {
            var posts = this.imageService.GetImageUrlsById(id);

            if (posts != null) {
                Profile profile = new Profile()
                {
                    UserName = userName,
                    Posts = posts
                };

                return profile;
            }

            return null;
        }
    }
}
