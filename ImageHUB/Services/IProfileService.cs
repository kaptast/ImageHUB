using ImageHUB.Entities;
using System.Collections.Generic;

namespace ImageHUB.Services
{
    public interface IProfileService
    {
        Profile GetProfileByID(string userID, string userName);
        IEnumerable<Profile> GetProfilesByName(string userName);
    }
}
