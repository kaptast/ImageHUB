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
        Profile GetProfileByID(string id, string userName);

        IEnumerable<Profile> GetAll();

        IEnumerable<Profile> GetAllByName(string name);

        void AddFriend(string userID, string friendID);

        void AcceptFriend(string userID, string friendID);

        FriendStatus IsFriendsWith(string userID, string friendID);
    }
}
