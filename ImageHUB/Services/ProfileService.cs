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
            
            if (profile != null){
                profile.Posts = this.imageService.GetImageUrlsById(context, profile.ID);
                return profile;
            }
            else {
                profile = new Profile(){
                    ID = id,
                    UserName = userName
                };

                context.AddNewProfile(profile);
            }

            return profile;
        }
    }
}
