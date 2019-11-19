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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            this.profileService = profileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public Repositories.Profile Get()
        {
            string userName = HttpContext.User.Identity.Name;
            string id = Hashes.ComputeSha256Hash(userName);
            //string id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = this.profileService.GetProfileByID(id, userName);
            profile.Avatar = id;

            return profile;
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize]
        public Repositories.Profile GetById(string id)
        {
            string userName = HttpContext.User.Identity.Name;

            //string userID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userID = Hashes.ComputeSha256Hash(userName);
            if (id.Equals("0"))
            {

                id = userID;
            }

            var profile = this.profileService.GetProfileByID(id, userName);
            profile.Avatar = id;

            if (profile.ID.Equals(userID))
            {
                profile.ShowFriendButton = false;
                profile.Status = FriendStatus.NotFriends;
            } else
            {
                profile.Status = this.profileService.IsFriendsWith(userID, profile.ID);
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