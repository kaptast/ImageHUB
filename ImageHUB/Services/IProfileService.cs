using ImageHUB.Entities;
using ImageHUB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IProfileService
    {
        Profile GetProfileByID(string userID, string userName);
        IEnumerable<Profile> GetProfilesByName(string userName);
    }
}
