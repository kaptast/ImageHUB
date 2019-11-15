using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private DatabaseContext context;

        public ProfileController(IProfileService profileService, DatabaseContext context)
        {
            this.profileService = profileService;
            this.context = context;
        }

        [HttpGet]
        public Repositories.Profile Get()
        {
            string userName = User.FindFirstValue(ClaimTypes.Name);
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);

            var profile = this.profileService.GetProfileByID(this.context, id, userName);
            profile.Avatar = id;
            profile.Email = email;

            return profile;
        }

        [HttpGet]
        [Route("GetById")]
        public Repositories.Profile GetById(string id)
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id.Equals("0"))
            {

                id = userID;
            }

            string userName = User.FindFirstValue(ClaimTypes.Name);

            var profile = this.profileService.GetProfileByID(this.context, id, userName);
            profile.Avatar = id;

            if (profile.ID.Equals(userID))
            {
                profile.ShowFriendButton = false;
                profile.IsFriend = false;
            } else
            {
                profile.IsFriend = this.profileService.IsFriendsWith(this.context, userID, profile.ID);
                profile.ShowFriendButton = !profile.IsFriend;
            }

            return profile;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Repositories.Profile> GetAll()
        {
            return this.profileService.GetAll(this.context);
        }

        [HttpGet]
        [Route("GetAllByName")]
        public IEnumerable<Repositories.Profile> GetAllByName(string name)
        {
            return this.profileService.GetAllByName(this.context, name);
        }
    }
}