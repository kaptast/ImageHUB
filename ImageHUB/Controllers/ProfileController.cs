using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageHUB.Entities;
using ImageHUB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IPostService postService;

        public ProfileController(IProfileService profileService, IPostService postService)
        {
            this.profileService = profileService;
            this.postService = postService;
        }

        [HttpGet]
        public Profile Get()
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = this.profileService.GetProfileByID(userId, userName);

            return profile;
        }

        [HttpGet]
        [Route("GetById")]
        public Profile GetById(string id)
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id.Equals("0"))
            {
                id = userId;
            }

            var profile = this.profileService.GetProfileByID(id, userName);
            profile.Posts = this.postService.GetPostsByUser(id);

            if (profile.UserID.Equals(userId))
            {
                profile.ShowFriendButton = false;
                profile.Status = FriendStatus.NotFriends;
            }
            else
            {
                profile.Status = this.profileService.IsFriendsWith(userId, profile.UserID);
                profile.ShowFriendButton = profile.Status == FriendStatus.NotFriends ? true : false;
            }

            return profile;
        }

        [HttpGet]
        [Route("GetAllByName")]
        public IEnumerable<Profile> GetAllByName(string name)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profiles = this.profileService.GetProfilesByName(name);

            foreach (var profile in profiles)
            {
                if (profile.UserID.Equals(userId))
                {
                    profile.ShowFriendButton = false;
                    profile.Status = FriendStatus.NotFriends;
                }
                else
                {
                    profile.Status = this.profileService.IsFriendsWith(userId, profile.UserID);
                    profile.ShowFriendButton = profile.Status == FriendStatus.NotFriends ? true : false;
                }
            }

            return profiles;
        }

        [HttpGet]
        [Route("GetNotifications")]
        public int GetNotifications(string id)
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null || (id != null && (id.Equals("0") || id.Equals(userId))))
            {
                return this.profileService.GetWaitingFriends(userId).Count();
            }

            return 0;
        }
    }
}