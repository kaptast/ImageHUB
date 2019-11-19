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
    public class FriendController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendController(IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            this.profileService = profileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("AddFriend")]
        [Authorize]
        public void AddFriend(string id)
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = Hashes.ComputeSha256Hash(userName);

            this.profileService.AddFriend(userId, id);
        }

        [HttpPost]
        [Route("AcceptFriend")]
        [Authorize]
        public void AcceptFriend(string id)
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = Hashes.ComputeSha256Hash(userName);

            this.profileService.AcceptFriend(userId, id);
        }
    }
}