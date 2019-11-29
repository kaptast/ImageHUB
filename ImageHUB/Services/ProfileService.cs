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

            return profile;
        }

        public IEnumerable<Profile> GetProfilesByName(string userName)
        {
            var profiles = this.repository.GetProfilesByName(userName);

            return profiles;
        }
    }
}
