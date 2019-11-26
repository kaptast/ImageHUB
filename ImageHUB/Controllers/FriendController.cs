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
    public class FriendController : ControllerBase
    {
        private readonly IProfileService profileService;

        public FriendController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPost]
        [Route("AddFriend")]
        [Authorize]
        public void AddFriend(string id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            System.Diagnostics.Trace.WriteLine(string.Format("AddFriend userID: {0}", userId));

            this.profileService.AddFriend(userId, id);
        }

        [HttpPost]
        [Route("AcceptFriend")]
        [Authorize]
        public void AcceptFriend(string id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.profileService.AcceptFriend(userId, id);
        }
    }
}