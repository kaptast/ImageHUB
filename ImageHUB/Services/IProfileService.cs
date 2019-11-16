using ImageHUB.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHUB.Services
{
    public interface IProfileService
    {
        Profile GetProfileByID(DatabaseContext context, string id, string userName);

        IEnumerable<Profile> GetAll(DatabaseContext context);

        IEnumerable<Profile> GetAllByName(DatabaseContext context, string name);

        void AddFriend(DatabaseContext context, string userID, string friendID);

        FriendStatus IsFriendsWith(DatabaseContext context, string userID, string friendID);
    }
}
