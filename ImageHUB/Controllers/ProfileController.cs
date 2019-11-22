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
    [Produces("application/json")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public Repositories.Profile Get()
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = this.profileService.GetProfileByID(userId, userName);
            profile.Avatar = userId;

            return profile;
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize]
        public Repositories.Profile GetById(string id)
        {
            string userName = HttpContext.User.Identity.Name;

            //string userID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id.Equals("0"))
            {

                id = userId;
            }

            var profile = this.profileService.GetProfileByID(id, userName);
            profile.Avatar = id;

            if (profile.ID.Equals(userId))
            {
                profile.ShowFriendButton = false;
                profile.Status = FriendStatus.NotFriends;
            } else
            {
                profile.Status = this.profileService.IsFriendsWith(userId, profile.UserID);
                profile.ShowFriendButton = profile.Status == FriendStatus.NotFriends ? true : false;
            }

            return profile;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Repositories.Profile> GetAll()
        {
            return this.profileService.GetAll();
        }

        [HttpGet]
        [Route("GetAllByName")]
        public IEnumerable<Repositories.Profile> GetAllByName(string name)
        {
            return this.profileService.GetAllByName(name);
        }
    }
}